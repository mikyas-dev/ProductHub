using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.User;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ProductHubDbContext _dbContext;

    public UserRepository(ProductHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public User? GetUserByIdAsync(Guid id)
    {
        var userId = new UserId(id);
        return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
    }

    public User? GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
    
}