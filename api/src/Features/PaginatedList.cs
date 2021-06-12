using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Features
{
    public record PaginatedList<T>
    {
        public PaginatedList(ICollection<T> items, int total, int page, int pageSize)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling(total / (double)pageSize);
            Count = items.Count;
            Total = total;
        }

        public PaginatedList(PaginatedList<T> copy)
        {
            Items = copy.Items;
            Page = copy.Page;
            PageSize = copy.PageSize;
            PageCount = copy.PageCount;
            Count = copy.Count;
            Total = copy.Total;
        }

        public int Page { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        public int Count { get; }
        public int Total { get; }

        public ICollection<T> Items { get; }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
