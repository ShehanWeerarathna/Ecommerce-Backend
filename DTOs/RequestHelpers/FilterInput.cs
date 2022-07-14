namespace Ecommerce_Backend.DTOs.RequestHelpers;

public class FilterInput
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; }
    public string SearchTerm { get; set; }
    public string Brands { get; set; }
    public string Types { get; set; }
    
}