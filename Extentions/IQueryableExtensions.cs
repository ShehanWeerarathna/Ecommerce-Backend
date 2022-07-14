namespace Ecommerce_Backend.Extentions;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObject)
    {
        if (queryObject.PageNumber <= 0)
            queryObject.PageNumber = 1;
        if (queryObject.PageSize <= 0)
            queryObject.PageSize = 10;

        return query.Skip((queryObject.PageNumber - 1) * queryObject.PageSize).Take(queryObject.PageSize);
    }
}