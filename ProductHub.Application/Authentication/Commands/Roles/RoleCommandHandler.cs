using ErrorOr;
using MediatR;
using ProductHub.Application.Common.Interfaces.Persistence;
using ProductHub.Domain.User;

namespace ProductHub.Application.Authentication.Commands.Roles;

public class RoleCommandHandler : IRequestHandler<RoleCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;
    
    public RoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(RoleCommand request, CancellationToken cancellationToken)
    {

        var user = _userRepository.GetUserByIdAsync(Guid.Parse(request.UserId));
        if (user is null)
        {
            return Domain.Common.Errors.Errors.User.UserNotFound;
        }

        user.UpdateRole(request.Role);
        await _userRepository.UpdateUser(user);
        return user;
    }
}