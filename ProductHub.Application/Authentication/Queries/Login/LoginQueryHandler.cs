using ProductHub.Application.Authentication.Common;

namespace ProductHub.Application.Authentication.Queries.Login;
using ProductHub.Application.Common.Interfaces.Authentication;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Common.Errors;
using ErrorOr;
using MediatR;

public class LoginQueryHandler : IRequestHandler< LoginQuery,ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    private readonly IUserRepository _userRepository;
    
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var user = _userRepository.GetUserByEmail(query.Email);
        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // check if the password is correct
        if (!PasswordService.VerifyPassword(query.Password, user.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        
        return new AuthenticationResult(
            user,
            token
        );
    }
}