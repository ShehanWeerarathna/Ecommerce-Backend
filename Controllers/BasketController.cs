using Ecommerce_Backend.Data;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Backend.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class BasketController:ControllerBase
{
    private readonly DataContext _context;
    public BasketController(DataContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetBasket")]
    public async Task<ActionResult<BasketDto>> GetBasket()
    {
        var basket = await RetrieveBasket();

        if (basket == null) return NotFound();
        
        return new BasketDto
        {
            Id = basket.Id,
            BuyerId = Guid.Parse(basket.BuyerId),
            Items = basket.Items.Select(item => new BasketItemDto
            {
                ProductId = item.ProductId,
                Name = item.Product.Name,
                Price = item.Product.Price,
                PictureUrl = item.Product.PictureUrl,
                Type = item.Product.Type,
                Brand = item.Product.Brand,
                Quantity = item.Quantity
            }).ToList()
        };
    }
    
    [HttpPost] //api/basket?productId=&quantity=
    public async Task<ActionResult<BasketDto>> AddItemToBasket(Guid productId, int quantity)
    {
        var basket = await RetrieveBasket();
        
        if (basket == null) basket = CreateBasket();
        
        var product = await _context.Products.FindAsync(productId);
        
        if (product == null) return BadRequest(new ProblemDetails{Title = "Product not found"});
        
        basket.AddItem(product,quantity);
        
        var result = await _context.SaveChangesAsync() > 0;
        //save changes
        if (result) return CreatedAtRoute("GetBasket",MapBasketToDto(basket));
        
        return BadRequest(new ProblemDetails {Title = "Problem saving items to basket"});
    }

    

    [HttpDelete]
    public async Task<ActionResult> RemoveBasketItem(Guid productId, int quantity)
    {
        var basket = await RetrieveBasket();

        if (basket == null) return NotFound();

        basket.RemoveItem(productId, quantity);

        var result = await _context.SaveChangesAsync() > 0;

        if (result) return Ok();

        return BadRequest(new ProblemDetails{Title = "Problem removing item from the basket"});
    }

    
    //-------------------------------------------------------------------------------//
    private async Task<Basket> RetrieveBasket()
    {
        return await _context.Baskets
            .Include(i => i.Items)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);

       
    }
    
    private Basket CreateBasket()
    {
        var buyerId = Guid.NewGuid().ToString();
        var cookieOption = new CookieOptions {IsEssential = true, Expires = DateTime.Now.AddDays(30)};
        Response.Cookies.Append("buyerId",buyerId,cookieOption);
        var basket = new Basket {BuyerId = buyerId};
        _context.Baskets.Add(basket);
        return basket;
    }

    private BasketDto MapBasketToDto(Basket basket)
    {
        return new BasketDto
        {
            Id = basket.Id,
            BuyerId = Guid.Parse(basket.BuyerId),
            Items = basket.Items.Select(item => new BasketItemDto
            {
                ProductId = item.ProductId,
                Name = item.Product.Name,
                Price = item.Product.Price,
                PictureUrl = item.Product.PictureUrl,
                Type = item.Product.Type,
                Brand = item.Product.Brand,
                Quantity = item.Quantity
            }).ToList()
        };
    }
}