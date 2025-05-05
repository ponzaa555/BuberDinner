using BuberDinner.Application.Common.Errors.OneOF;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts;
using Microsoft.AspNetCore.Mvc;
using OneOf;

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
            OneOf<AuthenticationResult , IError> registerResult = _authenticationService.Register(
               request.FirstName,
               request.LastName,
               request.Email,
               request.Password
            );
            
            /*
            if(registerResult.IsT0)
            {
                var authResult = registerResult.AsT0;
                AuthenticationResponse response = MapAuthResult(authResult);
                return Ok(response);
            } 
            return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists");
            อ่านค่อนข้างยาก สามารถแปลงเป็นแบบนี้ได้ ด้านล่าง 
            */
            // คือ เราจะใช้ OneOf<T0, T1> ลำดับ ของ T0 และ T1 จะต้องตรงกันdับกับ function ไหน match ถ้า T0 ถูกต้องจะไปที่ authResult
            return registerResult.Match(
               authResult => Ok(MapAuthResult(authResult)),
               error => Problem( statusCode: (int)error.StatusCode, title: error.Message)
            );
       }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                              authResult.User.Id,
                              authResult.User.FirstName,
                              authResult.User.LastName,
                              authResult.User.Email,
                              authResult.Token
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