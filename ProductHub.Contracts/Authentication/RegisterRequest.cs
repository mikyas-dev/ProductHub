namespace ProductHub.Contracts.Authentication;

public record RegisterRequest(
    string Email,
    string Password,
    string UserName
    );