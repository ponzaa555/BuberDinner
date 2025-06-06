using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSetting _jwtSetting;
        private readonly IDateTimeProvider _dateTimeProvider;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider , IOptions<JwtSetting> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;   
            _jwtSetting = jwtOptions.Value;
        }
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
            var signingCredentials = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName , user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName , user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSetting.ExpiryMinuts),
                claims: claims,
                signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}