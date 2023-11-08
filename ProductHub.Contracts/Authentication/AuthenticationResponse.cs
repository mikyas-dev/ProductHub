namespace ProductHub.Contracts.Authentication;

public record AuthenticationResponse(
    string Email,
    string Id,
    string UserName,
    string Token
    );

