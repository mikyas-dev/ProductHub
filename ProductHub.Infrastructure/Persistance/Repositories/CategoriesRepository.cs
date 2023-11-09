using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Category;
using ProductHub.Domain.Category.ValueObjects;

namespace ProductHub.Infrastructure.Persistance.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly ProductHubDbContext _dbContext;

    public CategoriesRepository(ProductHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == CategoryId.Create(id));
    }

    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }
}