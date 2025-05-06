using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller
{
    [Route("auth")]
    // this apply ErrorHandlingFilter to all AuthController
    //[ErrorHandlingFilter]
    public class AuthenticationController : ApiController
    {
     private readonly ISender _mediator;
     public AuthenticationController(ISender mediator )
     {
      _mediator = mediator;    
     }

       [HttpPost("register")]
       public async Task<IActionResult> Register([FromBody]RegisterRequest request )
        {
            var command  = new RegisterCommand(request.FirstName,
               request.LastName,
               request.Email,
               request.Password);            
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
           return authResult.Match(
                  authResult1 => Ok(MapAuth(authResult1)),
                  errors => Problem(errors)
           );
        }


        [HttpPost("login")]
       public async  Task<IActionResult> Login (LoginRequest request)
       {
         var Query = new LoginQuery(request.Email, request.Password);
          ErrorOr<AuthenticationResult>loginResult = _mediator.Send(Query).Result;
         if(loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredential)
         {
            return Problem(statusCode: StatusCodes.Status401Unauthorized ,  title: loginResult.FirstError.Description);
         }
          return loginResult.Match(
            loginResult => Ok(MapAuth(loginResult)),
            errors => Problem(errors)
          );
       }
      private static AuthenticationResponse MapAuth(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                           authResult.User.Id,
                           authResult.User.FirstName,
                           authResult.User.LastName,
                           authResult.User.Email,
                           authResult.Token
                        );
        }
    }
}