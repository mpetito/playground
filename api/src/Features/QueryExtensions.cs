using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Features
{
    public static class QueryExtensions
    {
        public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken ct = default)
            where TDestination : class => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, ct);

        public static Projection<TSource> Project<TSource>(this IQueryable<TSource> queryable)
            where TSource : class => new(queryable);

        public class Projection<TSource> where TSource : class
        {
            public IQueryable<TSource> Source { get; }

            public Projection(IQueryable<TSource> source) => Source = source;

            public IQueryable<TDestination> To<TDestination>(IConfigurationProvider configuration) =>
                Source.AsNoTracking().ProjectTo<TDestination>(configuration);
        }
    }
}
