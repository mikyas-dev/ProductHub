using ProductHub.Domain.User;

namespace ProductHub.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    void AddUser(User user);
    User? GetUserByIdAsync(Guid id);
    User? GetUserByEmail(string email);

    Task UpdateUser(User user);
}