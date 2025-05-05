using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters
{
    public class ErrorFilterProblemDetail : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            
            var problemDetail = new ProblemDetails
            {
                Type = "https://httpstatuses.com/500",
                Title = "An error occured while processing your request",
                Status =  (int)HttpStatusCode.InternalServerError,
                
            };
            
            context.Result = new ObjectResult(problemDetail);

            context.ExceptionHandled = true;
        }
    }
}