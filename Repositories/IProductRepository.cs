using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Repositories;

public interface IProductRepository
{
    Task<IList<Product>> GetProducts();
    Task<Product> GetProductById(Guid id);
    void CreateProduct(Product product);
}