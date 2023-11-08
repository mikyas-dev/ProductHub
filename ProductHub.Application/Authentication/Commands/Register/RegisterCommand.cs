using ProductHub.Application.Authentication.Common;
using MediatR;
using ErrorOr;

namespace ProductHub.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string UserName
) : IRequest<ErrorOr<AuthenticationResult>>;
