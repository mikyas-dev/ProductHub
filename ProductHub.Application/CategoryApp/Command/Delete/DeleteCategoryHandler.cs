using ErrorOr;
using MediatR;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Common.Errors;

namespace ProductHub.Application.CategoryApp.Command.Delete;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ErrorOr<string>>
{
    private readonly ICategoriesRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoriesRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);
        if (category is null)
        {
            return Errors.Category.CategoryNotFound;
        }

        await _categoryRepository.DeleteCategoryAsync(category);
        return $"Category with id {request.Id} was deleted successfully";
    }
}