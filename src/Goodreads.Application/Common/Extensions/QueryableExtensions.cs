using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Goodreads.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortColumn, string? sortOrder)
    {
        if (string.IsNullOrWhiteSpace(sortColumn))
            return query;

        var direction = sortOrder?.ToLower() == "desc" ? "descending" : "ascending";
        return query.OrderBy($"{sortColumn} {direction}");
    }

    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int? pageNumber, int? pageSize)
    {
        if (!pageNumber.HasValue || !pageSize.HasValue)
            return query;

        return query
            .Skip((pageNumber.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
    }

    public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, string[]? includes)
    where T : class
    {
        if (includes == null || includes.Length == 0)
            return query;

        foreach (var include in includes)
            query = query.Include(include);

        return query;
    }
}