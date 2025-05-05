using System.Net;
using System.Text.Json;

namespace BuberDinner.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // เหมือน go อย่างตอน เช็ค jwt header ที่ถอด token มาเช็คก่อนค่อยไปทำ call api อันต่อไป
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context ,ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context , Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new {error = "An error occured while processing your request"});
            // หลักการเหมือน go เลย context ก็เหมือน response.writter ที่เราสามารถเขียน hedaer status  code , body อะไร บลาๆ
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }
    }
}