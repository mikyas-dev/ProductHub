using ProductHub.Domain.Common.Models;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Domain.User;

public sealed class User : AggregateRoot<UserId>
{
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    public string Role { get; private set; } = "User";

    public User(UserId id, string userName, string email, string password) : base(id)
    {
        UserName = userName;
        Email = email;
        Password = password;
        
    }

    public void UpdateRole(string role)
    {
        Role = role;
    }
    
    public static User Create(string userName, string email, string password)
    {
        return new(UserId.CreateUnique(), userName, email, password);
    }
    
    public void Update(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
    
    public void UpdatePassword(string password)
    {
        Password = password;
    }

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}