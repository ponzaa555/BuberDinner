using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts;
using Mapster;

namespace BuberDinner.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // This 2 line are redundant
        config.NewConfig<RegisterRequest , RegisterCommand>();
        config.NewConfig<LoginRequest , LoginQuery>();


        config.NewConfig<AuthenticationResult , AuthenticationResponse>()
        // .Map(dest => dest.Token , src => src.Token)
        .Map(dest => dest , src => src.User);
    }
}