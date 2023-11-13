using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Product;
using ProductHub.Domain.Product.ValueObjects;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Infrastructure.Persistance.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductHubDbContext _dbContext;

    public ProductRepository(ProductHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddProductAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        // reload the product with the user and category
        await _dbContext.Entry(product).Reference(p => p.User).LoadAsync();
        await _dbContext.Entry(product).Reference(p => p.Category).LoadAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == ProductId.Create(id));
        if (product == null)
        {
            return null;
        }

        await _dbContext.Entry(product).Reference(p => p.User).LoadAsync();
        await _dbContext.Entry(product).Reference(p => p.Category).LoadAsync();
        return product;
    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var products = await _dbContext.Products.ToListAsync();
        foreach (var product in products)
        {
            await _dbContext.Entry(product).Reference(p => p.User).LoadAsync();
            await _dbContext.Entry(product).Reference(p => p.Category).LoadAsync();
        }

        return products;
    }

    public async Task UpdateProductAsync(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();

        await _dbContext.Entry(product).Reference(p => p.User).LoadAsync();
        await _dbContext.Entry(product).Reference(p => p.Category).LoadAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Product>> GetProductsByUserIdAsync(UserId userId)
    {
        var products = await _dbContext.Products.Where(p => p.UserId == userId).ToListAsync();
        foreach (var product in products)
        {
            await _dbContext.Entry(product).Reference(p => p.User).LoadAsync();
            await _dbContext.Entry(product).Reference(p => p.Category).LoadAsync();
        }

        return products;
    }
}