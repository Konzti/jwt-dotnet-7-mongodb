using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using JwtDotNet7.Models;
using JwtDotNet7.Services.Interfaces;

namespace JwtDotNet7.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class Users : ControllerBase
    {
        private readonly IUserService _UserService;
        public Users(IUserService UserService)
        {
            _UserService = UserService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return Ok(_UserService.GetUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            return Ok(_UserService.GetUserById(id));
        }
    }
}