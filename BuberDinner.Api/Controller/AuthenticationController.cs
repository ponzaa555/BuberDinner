
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller
{
    [ApiController]
    [Route("auth")]
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
            var authResult = _authenticationService.Register(
               request.FirstName,
               request.LastName,
               request.Email,
               request.Password
            );
            var response = new AuthenticationResponse(
               authResult.Id,
               authResult.FirstName,
               authResult.LastName,
               authResult.Email,
               authResult.Token
            );
            return Ok(response);
       }
       
       [HttpPost("login")]
       public IActionResult Login (LoginRequest request)
       {
          var loginResult = _authenticationService.Login(request.Email , request.Password);
          var response = new AuthenticationResponse(
               loginResult.Id,
               loginResult.FirstName,
               loginResult.LastName,
               loginResult.Email,
               loginResult.Token
            );
            return Ok(response);
       }
    }
}