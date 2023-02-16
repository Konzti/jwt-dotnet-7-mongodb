using JwtDotNet7.Models.DTOs;
using JwtDotNet7.Models.Results;

namespace JwtDotNet7.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<RegisterResult> Register(RegisterDto registerDto);
        public LoginResult Login(LoginDto loginDto);
    }
}