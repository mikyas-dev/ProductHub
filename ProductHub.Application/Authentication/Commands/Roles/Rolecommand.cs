using ErrorOr;
using MediatR;
using ProductHub.Domain.User;

namespace ProductHub.Application.Authentication.Commands.Roles;

public record RoleCommand(
    string UserId,
    string Role
) : IRequest<ErrorOr<User>>;