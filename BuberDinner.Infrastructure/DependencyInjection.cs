// this file for register all of service in application layers

using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection  AddInfrastructure( 
            this IServiceCollection services ,
            IConfiguration configuration 
        )
        {
            services.AddAuth(configuration);
            services.AddSingleton<IDateTimeProvider , DateTimeProvider>();

            // add Persistence
            services.AddPersistence(configuration);
            
            return services;
        }
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUserRepository , UserRepository>();
            services.AddScoped<IMenuRepository , MenuRepository>();
            return services;
        }
        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // setUp JWT
            var JwtSetting = new JwtSetting();
            configuration.Bind(JwtSetting.SectionName,JwtSetting);
            // services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.SectionName));
            services.AddSingleton(Options.Create(JwtSetting));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            // configure authentication 
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtSetting.Issuer,
                    ValidAudience = JwtSetting.Audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(JwtSetting.Secret)),
                });
            return services;
        }
    } 
}