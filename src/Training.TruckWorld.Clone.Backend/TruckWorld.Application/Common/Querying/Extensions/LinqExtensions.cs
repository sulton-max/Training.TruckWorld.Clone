
using TruckWorld.Application.Common.Models.Querying;

namespace TruckWorld.Application.Common.Querying.Extensions;

/// <summary>
/// created two extension methods to do Paginations
/// </summary>
public static class LinqExtensions
{
    public static IQueryable<TSource> ApplyPagination<TSource>(this IQueryable<TSource> source, FilterPagination paginationOptions)
    {
        return source.Skip((int)((paginationOptions.PageToken - 1) * paginationOptions.PageSize)).Take((int)paginationOptions.PageSize);
    }

    public static IEnumerable<TSource> ApplyPagination<TSource>(this IEnumerable<TSource> source, FilterPagination paginationOptions)
    {
        return source.Skip((int)((paginationOptions.PageToken - 1) * paginationOptions.PageSize)).Take((int)paginationOptions.PageSize);
    }
}
