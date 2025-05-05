using BuberDinner.Api.Error;
using BuberDinner.Api.Filters;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add dependency Injection
{
    // option => option.Filters.Add<ErrorHandlingFilterAttribute>() add ErrorHandlingFilter to all controller
    // builder.Services.AddControllers(option => option.Filters.Add<ErrorFilterProblemDetail>());
    builder.Services.AddControllers();
    //services.TryAddSingleton<ProblemDetailsFactory, DefaultProblemDetailsFactory>();
    // แก้ base libery ของ C# 
    builder.Services.AddSingleton<ProblemDetailsFactory, BuberDinerDetailFactory>();
    builder.Services
        .AddApplication()
        // Pass config to Interface layers
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseMiddleware<ErrorHandlingMiddleware>();

/* what this line do 
 Adds a middleware to the pipeline that will catch exceptions, 
 log them, reset the request path, and re-execute the request
*/
{
    //ถ้า มีการ throw exception ใน controller จะ call ไปที่ controller error
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

