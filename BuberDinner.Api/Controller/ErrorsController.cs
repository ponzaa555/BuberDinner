using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            return Problem(
                title : exception? .Message,
                 statusCode:400
            );
        }
    }
}