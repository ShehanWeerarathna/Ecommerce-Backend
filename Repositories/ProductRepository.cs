using Ecommerce_Backend.Data;
using Ecommerce_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Backend.Repositories;

public class ProductRepository:IProductRepository
{
    private readonly DataContext context;

    public ProductRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<IList<Product>> GetProducts()
    {
        return await context.Products.AsNoTracking().ToListAsync();
    }
    public async Task<Product> GetProductById(Guid id)
    {
        return await context.Products.Where(s => s.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public void CreateProduct(Product product)
    {
        context.Products.Add(product);
    }
    
    
}