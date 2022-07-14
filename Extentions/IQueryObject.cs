namespace Ecommerce_Backend.Extentions;

public interface IQueryObject
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
}