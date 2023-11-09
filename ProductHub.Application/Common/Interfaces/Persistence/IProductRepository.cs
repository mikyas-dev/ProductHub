using ProductHub.Domain.Product;

namespace ProductHub.Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<List<Product>> GetProductsAsync();
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
}