
using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Interfaces.Error;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller
{
    [ApiController]
    [Route("auth")]
    // this apply ErrorHandlingFilter to all AuthController
    //[ErrorHandlingFilter]
    public class AuthenticationController : ControllerBase
    {
     private readonly IAuthenticationService _authenticationService;
     public AuthenticationController(IAuthenticationService authenticationService)
     {
          _authenticationService = authenticationService;
     }

       [HttpPost("register")]
       public IActionResult Register([FromBody]RegisterRequest request )
        {
            Result<AuthenticationResult> registerResult = _authenticationService.Register(
               request.FirstName,
               request.LastName,
               request.Email,
               request.Password
            );
            if(registerResult.IsSuccess)
            {
               return Ok(MapAuthResult(registerResult.Value));
            }
            var firstError = registerResult.Errors[0];

            if (firstError is DuplicateEmailError)
            {
                return Problem(statusCode: StatusCodes.Status409Conflict,title: "Duplicate Email",detail: "User with given email already exists.");
            }

            return Problem();
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult registerResult)
        {
            return new AuthenticationResponse(
               registerResult.User.Id,
               registerResult.User.FirstName,
               registerResult.User.LastName,
               registerResult.User.Email,
               registerResult.Token
            );
        }

        [HttpPost("login")]
       public IActionResult Login (LoginRequest request)
       {
          var loginResult = _authenticationService.Login(request.Email , request.Password);
          var response = new AuthenticationResponse(
               loginResult.User.Id,
               loginResult.User.FirstName,
               loginResult.User.LastName,
               loginResult.User.Email,
               loginResult.Token
            );
            return Ok(response);
       }
    }
}