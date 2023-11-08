namespace ProductHub.Contracts.Users
{
    public record RoleRequest(
        string UserId,
        string Role
    );
}