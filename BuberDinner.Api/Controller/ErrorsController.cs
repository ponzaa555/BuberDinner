using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BuberDinner.Api.Controller
{
    public class ErrorsController : ControllerBase
    { 
        [Route("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Exception?  exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            // use switch case with type Exception
            var (statusCode , message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError , "An error occurred while processing your request."),
            };
            Console.WriteLine(message);
            return Problem(
                statusCode : statusCode,
                title : message
            );
        }
    }
}