
namespace ProductHub.Application.Authentication.Common;
using ProductHub.Domain.User;

public record AuthenticationResult(
    User User,
    string Token
);