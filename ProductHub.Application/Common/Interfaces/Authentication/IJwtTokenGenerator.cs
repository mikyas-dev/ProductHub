using ProductHub.Domain.User;

namespace ProductHub.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}