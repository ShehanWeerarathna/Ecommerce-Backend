using Ecommerce_Backend.Extentions;

namespace Ecommerce_Backend.DTOs.RequestHelpers;

public class ProductParams : PaginationParams
{
    public string? OrderBy { get; set; }
    public string? SearchTerm { get; set; }
    public string? Brands { get; set; }
    public string? Types { get; set; }
}