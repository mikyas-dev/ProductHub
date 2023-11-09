using ErrorOr;
using MediatR;
using ProductHub.Application.CategoryApp.Common;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Category;
using ProductHub.Domain.Category.ValueObjects;
using ProductHub.Domain.Common.Errors;


namespace ProductHub.Application.CategoryApp.Command.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<CategoryResult>>
{
    private readonly ICategoriesRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoriesRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<CategoryResult>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var categoryExists = await _categoryRepository.GetCategoryByNameAsync(request.Name);
        if (categoryExists is not null)
        {
            return Errors.Category.NameIsAlreadyTaken;
        }

        var category = new Category(
            CategoryId.CreateUnique(),
            request.Name,
            request.Description
        );

        await  _categoryRepository.AddCategoryAsync(category);

        return new CategoryResult(
            category.Id.Value,
            category.Name,
            category.Description
        );
    }
}