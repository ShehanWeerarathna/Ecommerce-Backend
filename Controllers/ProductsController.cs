using System.Text.Json;
using Ecommerce_Backend.Data;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.DTOs.RequestHelpers;
using Ecommerce_Backend.Extentions;
using Ecommerce_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Backend.Controllers;

public class ProductsController: BaseApiController
{
    private readonly DataContext _context;
    public ProductsController(DataContext context)
    {
        _context = context;
    }
    
    
    [HttpPost("GetAllProducts")]
    public async Task<ActionResult<List<Product>>> GetAllProducts(ProductParams productParams)
    {
        var query =  _context.Products.Sort(productParams.OrderBy).Search(productParams.SearchTerm).Filter(productParams.Brands,productParams.Types).AsQueryable();

        /*query = query.ApplyPaging(productParams);
        return Ok(query);*/
        var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);
        Response.Headers.Add("Pagination", JsonSerializer.Serialize(products.MetaData));
        return products;
    }
    
    [HttpGet]
    public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams)
    {
        var query = _context.Products
            .Sort(productParams.OrderBy)
            .Search(productParams.SearchTerm)
            .Filter(productParams.Brands, productParams.Types)
            .AsQueryable();

        var products = await PagedList<Product>.ToPagedList(query, 
            productParams.PageNumber, productParams.PageSize);

        Response.AddPaginationHeader(products.MetaData);

        return products;
    }


    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        var guid = GetNewGuidString();

        if (product == null) return NotFound();

        return product;
    }
    
    private static string GetNewGuidString()
    {
        return Guid.NewGuid().ToString();
    }

    [HttpGet("filters")]
    public async Task<IActionResult> GetFilters()
    {
        var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
        return Ok(new {brands,types});
    }

}