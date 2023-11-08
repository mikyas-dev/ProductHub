using ProductHub.Application.Authentication.Commands.Register;
using ProductHub.Application.Authentication.Common;
using ProductHub.Application.Authentication.Queries.Login;
using ProductHub.Contracts.Authentication;
using Mapster;
using ProductHub.Contracts.Users;
using ProductHub.Domain.User;

namespace ProductHub.Api.Common.Mappings;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<LoginRequest, LoginQuery>();
        
        config.NewConfig<RegisterRequest, RegisterCommand>();
        
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User)
            .Map(dest => dest.Id, src => src.User.Id.Value);
        
        config.NewConfig<User, RoleResponse>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}