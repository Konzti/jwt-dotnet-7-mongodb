using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using JwtDotNet7.Models;
using JwtDotNet7.Services.Interfaces;
using JwtDotNet7.Settings.Jwt;

namespace JwtDotNet7.Services
{
    public class JwtService : IJwtService
    {
        private readonly IJwtSettings _settings;
        public JwtService(IJwtSettings jwtSettings)
        {
            _settings = jwtSettings;
        }
        public string GenerateJwt(User user)
        {
            var key = Encoding.UTF8.GetBytes(_settings.JwtSecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),

                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_settings.ExpiryInMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            }));

            return token;
        }
    }
}