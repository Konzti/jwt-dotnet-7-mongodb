using JwtDotNet7.Models;
using JwtDotNet7.Models.DTOs;

namespace JwtDotNet7.Services.Interfaces
{
    public interface IUserService
    {
        public List<User> GetUsers();
        public User GetUserById(string id);

    }
}