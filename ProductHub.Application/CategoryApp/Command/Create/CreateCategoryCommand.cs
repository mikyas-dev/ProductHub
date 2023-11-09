using ErrorOr;
using MediatR;
using ProductHub.Application.CategoryApp.Common;

namespace ProductHub.Application.CategoryApp.Command.Create;

public record CreateCategoryCommand(
    string Name,
    string Description
) : IRequest<ErrorOr<CategoryResult>>;