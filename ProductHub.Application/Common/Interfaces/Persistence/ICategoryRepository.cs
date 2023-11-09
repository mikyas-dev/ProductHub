using ProductHub.Domain.Category;

namespace ProductHub.Application.Common.Interfaces.Persistence;

public interface ICategoriesRepository
{
    Task AddCategoryAsync(Category category);
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<Category?> GetCategoryByNameAsync(string name);
    Task<List<Category>> GetCategoriesAsync();
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}