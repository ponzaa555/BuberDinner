using BuberDinner.Api.Filters;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator , IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // 1) Validate user exist 
            if(_userRepository.GetUserByEmail(email) is not User user)
            {
                // ถึงจะ throw Exception แบบนี้ออกไปมันก็ยังโชว์ error อื่นๆของระบบด้วย
                return Errors.Authentication.InvalidCredential;
            }
            // 2) Validate password is correct
            if(user.Password != password)
            {
                return new[] {Errors.Authentication.InvalidCredential};
            }
            // 3) Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user,
                token
            );
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // 1. Validate user doesn't exist
            if(_userRepository.GetUserByEmail(email) is not null){
                return Errors.User.DuplicateEmail;
            }

            // 2. Create user (generate unique ID) & Persist to DB
            var user = new User{
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.AddUser(user);
            // 3.  Create JWT token           
            var token = _jwtTokenGenerator.GenerateToken(user);


            return new AuthenticationResult(
                user,
                token
            );
        }
    }
}