using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Querys
{
    public interface IAuthenticationServiceQuery
    {
        ErrorOr<AuthenticationResult> Login(string email, string password);
    }
}