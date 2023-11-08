using ProductHub.Application.Authentication.Common;
using ProductHub.Application.Common.Interfaces.Authentication;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.Common.Errors;
using ProductHub.Domain.User;


namespace ProductHub.Application.Authentication.Commands.Register;

using ProductHub.Domain.User.ValueObjects;
using ErrorOr;
using MediatR;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    private readonly IUserRepository _userRepository;
    
    
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        
        // check if the user already exists
        await Task.CompletedTask;
        var user = _userRepository.GetUserByEmail(command.Email);
        if (user is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        // create user (generate unique id) & persist to database
        var newUser = new User(
            UserId.CreateUnique(),
            command.UserName,
            command.Email,
            PasswordService.HashPassword(command.Password)
        );
        
        _userRepository.AddUser(newUser);
        
        // create JWT token
        var token = _jwtTokenGenerator.GenerateToken(newUser);
        
        // create password hash


        return new AuthenticationResult(
            newUser,
            token
        );
    }
}