using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Services;

public interface IProductService
{
    Task<IList<Product>> GetProducts();
    Task<Product> GetProductById(Guid id);
    Task<Product> CreateProduct(Product product);
}