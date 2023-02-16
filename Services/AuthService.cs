using JwtDotNet7.Models;
using JwtDotNet7.Models.DTOs;
using JwtDotNet7.Models.Results;
using JwtDotNet7.Services.Interfaces;
using JwtDotNet7.Settings.MongoDB;

using MongoDB.Driver;

namespace JwtDotNet7.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMongoCollection<User> _Users;
        private readonly IJwtService _jwtService;

        public AuthService(IMongoDBSettings settings, MongoClientBase client, IJwtService jwtService)
        {
            _Users = client.GetDatabase(settings.DatabaseName).GetCollection<User>(settings.CollectionName);
            _jwtService = jwtService;
        }

        public LoginResult Login(LoginDto loginDto)
        {

            var user = _Users.Find(User => User.Email == loginDto.Email).FirstOrDefault();
            if (user is null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return new LoginResult
                {
                    Error = "Invalid credentials"
                };
            };

            var token = _jwtService.GenerateJwt(user);
            return new LoginResult
            {
                Success = true,
                Token = token
            };
        }

        public async Task<RegisterResult> Register(RegisterDto registerDto)
        {

            if (_Users.Find(User => User.Email == registerDto.Email).FirstOrDefault() is not null)
            {
                return new RegisterResult
                {
                    Error = "Email already exists"
                };
            };

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
            await _Users.InsertOneAsync(user);
            return new RegisterResult { Success = true, User = user };
        }

    }
}