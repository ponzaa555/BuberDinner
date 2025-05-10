using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using Mapster;

namespace BuberDinner.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuRequest Requset , string HostId) , CreateMenuCommand>()
            .Map(des => des.HostId , src => src.HostId)
            .Map(des => des , src => src.Requset);
    }
}
