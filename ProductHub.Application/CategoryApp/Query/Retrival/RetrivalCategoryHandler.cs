using ErrorOr;
using MediatR;
using ProductHub.Application.CategoryApp.Common;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Common.Errors;

namespace ProductHub.Application.CategoryApp.Query.Retrival;

public class RetrivalCategoryHandler : IRequestHandler<RetrivalCategoryQuery, ErrorOr<List<CategoryResult>>>
{
    private readonly ICategoriesRepository _categoryRepository;

    public RetrivalCategoryHandler(ICategoriesRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<List<CategoryResult>>> Handle(RetrivalCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetCategoriesAsync();
        if (categories is null)
        {
            return Errors.Category.CategoryNotFound;
        }

        var categoryResults = categories.Select(category => new CategoryResult(
            category.Id.Value,
            category.Name,
            category.Description
        )).ToList();

        return categoryResults;
    }
}