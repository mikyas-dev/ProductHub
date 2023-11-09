using ErrorOr;
using MediatR;
using ProductHub.Application.CategoryApp.Common;

namespace ProductHub.Application.CategoryApp.Command.Update;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string Description
) : IRequest<ErrorOr<CategoryResult>>;