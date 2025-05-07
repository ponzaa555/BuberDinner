// this file for register all of service in application layers

using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BuberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMapping();
            // option => option.Filters.Add<ErrorHandlingFilterAttribute>() add ErrorHandlingFilter to all controller
            // builder.Services.AddControllers(option => option.Filters.Add<ErrorFilterProblemDetail>());
            services.AddControllers();
            //services.TryAddSingleton<ProblemDetailsFactory, DefaultProblemDetailsFactory>();
            // แก้ base libery ของ C# 
            services.AddSingleton<ProblemDetailsFactory, BuberDinerDetailFactory>();
            return services;
        }
    }
}