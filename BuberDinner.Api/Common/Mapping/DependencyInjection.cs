using System.Reflection;
using BuberDinner.Application;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Api.Common.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            /*
                try to wire all of mapping config in this file
                1. Scan all assembly that implement IRegister
                2. Register all of mapping config
            */
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
    }
}