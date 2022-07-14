namespace Ecommerce_Backend.DTOs;

public class AddItemToBasketInput
{
    public Guid ProductId { get; set; }
    public Guid BuyerId { get; set; }
    public int Quantity { get; set; }
}