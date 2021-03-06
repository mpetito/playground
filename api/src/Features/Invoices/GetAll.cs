using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Playground.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Features.Invoices
{
    public static class GetAll
    {
        public record Query : IRequest<Result>
        {
            public int Page { get; init; } = 1;
            public int PageSize { get; init; } = 10;
        }

        public record Result : PaginatedList<Model>
        {
            public Result(PaginatedList<Model> list)
                : base(list)
            {
            }
        }

        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(m => m.Page).GreaterThan(0);
                RuleFor(m => m.PageSize).InclusiveBetween(1, 1000);
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly PlaygroundDbContext _db;
            private readonly IConfigurationProvider _mapping;

            public Handler(PlaygroundDbContext db, IConfigurationProvider mapping)
            {
                _db = db;
                _mapping = mapping;
            }

            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var models = await _db.Invoices
                    .OrderBy(c => c.Id)
                    .ProjectTo<Model>(_mapping)
                    .ToPaginatedListAsync(request.Page, request.PageSize, cancellationToken);

                return new (models);
            }
        }
    }
}
