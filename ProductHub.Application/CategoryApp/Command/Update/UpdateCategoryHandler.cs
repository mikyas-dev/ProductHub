using ErrorOr;
using MediatR;
using ProductHub.Application.CategoryApp.Common;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Common.Errors;

namespace ProductHub.Application.CategoryApp.Command.Update;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ErrorOr<CategoryResult>>
{
    private readonly ICategoriesRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoriesRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<CategoryResult>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
        if (category is null)
        {
            return  Errors.Category.CategoryNotFound;
        }

        category.Update(request.Name, request.Description);

        await _categoryRepository.UpdateCategoryAsync(category);

        return new CategoryResult(
            category.Id.Value,
            category.Name,
            category.Description
        );
    }
}