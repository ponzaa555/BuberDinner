
using BuberDinner.Api.Filters;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller
{
    [Route("auth")]
    // this apply ErrorHandlingFilter to all AuthController
    //[ErrorHandlingFilter]
    public class AuthenticationController : ApiController
    {
     private readonly IAuthenticationService _authenticationService;
     public AuthenticationController(IAuthenticationService authenticationService)
     {
          _authenticationService = authenticationService;
     }

       [HttpPost("register")]
       public IActionResult Register([FromBody]RegisterRequest request )
        {
            ErrorOr<AuthenticationResult> authResult = _authenticationService.Register(
               request.FirstName,
               request.LastName,
               request.Email,
               request.Password
            );
           return authResult.Match(
                  authResult1 => Ok(MapAuth(authResult1)),
                  errors => Problem(errors)
           );
        }


        [HttpPost("login")]
       public IActionResult Login (LoginRequest request)
       {
          ErrorOr<AuthenticationResult>loginResult = _authenticationService.Login(
              request.Email,
              request.Password);
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