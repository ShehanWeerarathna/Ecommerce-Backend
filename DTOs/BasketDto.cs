namespace Ecommerce_Backend.DTOs;

public class BasketDto
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public List<BasketItemDto> Items { get; set; }
}