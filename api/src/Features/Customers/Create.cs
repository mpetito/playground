using FluentValidation;
using MediatR;
using Playground.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Features.Customers
{
    public static class Create
    {
        public record Form(string Name);

        public record Command(Form Form) : IRequest<Result>;

        public record Result(int Id);

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

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly PlaygroundDbContext _db;

            public Handler(PlaygroundDbContext db)
            {
                _db = db;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var customer = new Customer(request.Form);

                await _db.Customers.AddAsync(customer, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return new (customer.Id);
            }
        }
    }
}
