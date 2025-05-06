using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Command
{
    public interface IAuthenticationServiceCommand
    {
        ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);

    }
}