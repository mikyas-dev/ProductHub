using ErrorOr;
using MediatR;

namespace ProductHub.Application.CategoryApp.Command.Delete;

public record DeleteCategoryCommand(
    Guid Id
): IRequest<ErrorOr<string>>;