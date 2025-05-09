using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller
{
    [Route("auth")]
    [AllowAnonymous]
    // this apply ErrorHandlingFilter to all AuthController
    //[ErrorHandlingFilter]
    public class AuthenticationController : ApiController
    {
     private readonly ISender _mediator;
     private readonly IMapper _mapper;
     public AuthenticationController(ISender mediator , IMapper mapper)
     {
      _mapper = mapper;
      _mediator = mediator;    
     }

       [HttpPost("register")]
       public async Task<IActionResult> Register([FromBody]RegisterRequest request )
        {
            var command  = _mapper.Map<RegisterCommand>(request);       
        //     var errors = new List<Error>
        // {
        //     Error.Validation("InvalidEmail", "Email must be a valid email address."),
        //     Error.Validation("WeakPassword", "Password must be at least 8 characters.")
        // };

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);
           return authResult.Match(
                  authResult1 => Ok(MapAuth(authResult1)),
                  errors => Problem(errors)
           );
        }


        [HttpPost("login")]
       public async  Task<IActionResult> Login (LoginRequest request)
       {
         var Query = _mapper.Map<LoginQuery>(request);
          ErrorOr<AuthenticationResult>loginResult = await _mediator.Send(Query);
         if(loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredential)
         {
            return Problem(statusCode: StatusCodes.Status401Unauthorized ,  title: loginResult.FirstError.Description);
         }
          return loginResult.Match(
            // AuthenticationResponse กับ loginResult struct ไม่เหมือนกััน
            loginResult => Ok(_mapper.Map<AuthenticationResponse>(loginResult)),
            errors => Problem(errors)
          );
       }

      // manual map
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