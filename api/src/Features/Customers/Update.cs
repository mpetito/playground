using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Playground.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Features.Customers
{
    public static class Update
    {
        public record Form(string Name, string? Phone, Address Address); 

        public record Query(int Id) : IRequest<Context>;

        public record Context(Customer Customer);

        public record Command(Form Form, Context Context) : IRequest<Result>;

        public record Result();

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(m => m.Form).SetValidator(new FormValidator());
            }
        }

        public class FormValidator : AbstractValidator<Form>
        {
            public FormValidator()
            {
                RuleFor(m => m.Name).NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, Context>
        {
            private readonly PlaygroundDbContext _db;

            public QueryHandler(PlaygroundDbContext db)
            {
                _db = db;
            }

            public async Task<Context> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = await _db.Customers
                    .SingleAsync(c => c.Id == request.Id, cancellationToken);

                return new Context(customer);
            }
        }

        public class CommandHandler : IRequestHandler<Command, Result>
        {
            private readonly PlaygroundDbContext _db;

            public CommandHandler(PlaygroundDbContext db)
            {
                _db = db;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var (form, context) = request;
                var customer = context.Customer;
                
                customer.Update(form);

                await _db.SaveChangesAsync(cancellationToken);

                return new ();
            }
        }
    }
}
