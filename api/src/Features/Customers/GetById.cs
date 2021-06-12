using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Playground.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Features.Customers
{
    public static class GetById
    {
        public record Query(int Id) : IRequest<Result>;

        public record Result(Model Customer);

        public record Model(int Id, string Name, string? Phone, Address Address)
        {
            public class Mapping : Profile
            {
                public Mapping() => CreateMap<Customer, Model>();
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
                var model = await _db.Customers
                    .Where(c => c.Id == request.Id)
                    .Project().To<Model>(_mapping)
                    .SingleAsync(cancellationToken);

                return new (model);
            }
        }
    }
}
