using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Querys
{
    public class AuthenticationServiceQuery : IAuthenticationServiceQuery
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationServiceQuery(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // 1) Validate user exist 
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                // ถึงจะ throw Exception แบบนี้ออกไปมันก็ยังโชว์ error อื่นๆของระบบด้วย
                return Errors.Authentication.InvalidCredential;
            }
            // 2) Validate password is correct
            if (user.Password != password)
            {
                return new[] { Errors.Authentication.InvalidCredential };
            }
            // 3) Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user,
                token
            );
        }
    }
}