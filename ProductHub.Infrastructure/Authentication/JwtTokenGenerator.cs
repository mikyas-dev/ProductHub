using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProductHub.Application.Common.Interfaces.Authentication;
using ProductHub.Application.Common.Services;
using ProductHub.Domain.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ProductHub.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateProvider;

    private readonly JwtSettings _jwtSettings;
    
    public JwtTokenGenerator(IDateTimeProvider dateProvider , IOptions<JwtSettings> jwtOptions)
    {
        _dateProvider = dateProvider;
        _jwtSettings = jwtOptions.Value;
    }
  
    
    
    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256Signature
        );
        var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Typ, user.Role.ToString())
            };
        
        var securityKey = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: _dateProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: signingCredentials
            );
        
var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(securityKey);
        return token;
        
    }

    public string? GetUserIdFromClaim ()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.ReadJwtToken(_jwtSettings.Secret);
        var claims = token.Claims;
        var userId = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        return userId;        
    }
}