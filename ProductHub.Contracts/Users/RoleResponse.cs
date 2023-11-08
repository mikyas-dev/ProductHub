namespace ProductHub.Contracts.Users
{
    public record RoleResponse(
        string Email,
        string Id,
        string UserName,
        string Role  
    );
}