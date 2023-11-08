using ProductHub.Application.Authentication.Common;
using MediatR;

namespace ProductHub.Application.Authentication.Queries.Login;
using ErrorOr;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;