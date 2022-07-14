using Ecommerce_Backend.Models;
using Ecommerce_Backend.Persistence;
using Ecommerce_Backend.Repositories;

namespace Ecommerce_Backend.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }
    public async Task<Product> GetProductById(Guid id)
    {
        var existingProduct =  await _productRepository.GetProductById(id);
        

        return existingProduct;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        product.Id=Guid.NewGuid();
        _productRepository.CreateProduct(product);
        await _unitOfWork.CompleteAsync();
        return await GetProductById(product.Id);
    }
}