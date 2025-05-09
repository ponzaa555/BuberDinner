using BuberDinner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDinner.Api.Controller;


[Route("errors")]
[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    // our problem method which insted of receving the problem detail it will receive list error hear
    public IActionResult Problem(List<Error> errors)
    {
        //if no errors
        if (errors.Count == 0)
        {
            return Problem();
        }
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }
        // if (errors.All(errors => errors.NumericType == 23))
        // {
        //     return ValidationProblem(errors);
        // }
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors[0];
        return  ConverErrorToProblem(firstError);
    }
    //take single error and return the problem
    private IActionResult ConverErrorToProblem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem (List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description
            );
        }
        return ValidationProblem(modelStateDictionary);
    }
}