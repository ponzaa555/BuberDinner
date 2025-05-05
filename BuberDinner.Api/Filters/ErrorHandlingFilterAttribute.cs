using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = exception.Message;

            if (exception is AppException appException)
            {
                statusCode = appException.StatusCode;
            }

            // context.Result is get or set IActionResult is same as return in contriller Ok(something)
            context.Result = new ObjectResult(new {error = message}){
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}