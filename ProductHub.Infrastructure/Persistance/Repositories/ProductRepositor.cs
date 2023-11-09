using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Product;
using ProductHub.Domain.Product.ValueObjects;

namespace ProductHub.Infrastructure.Persistance.Repositories;

public class productRepository : IProductRepository
{
    private readonly ProductHubDbContext _dbContext;

    public productRepository(ProductHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddProductAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == ProductId.Create(id));
    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}