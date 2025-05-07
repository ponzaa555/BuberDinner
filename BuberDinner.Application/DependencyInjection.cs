// this file for register all of service in application layers

using System.Reflection;
using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Common.Behaviors;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication( this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            //services.AddScoped<IValidator<RegisterCommand> , RegisterCommandValidator>();
            // after install FluentValidation.AspNetCore
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}