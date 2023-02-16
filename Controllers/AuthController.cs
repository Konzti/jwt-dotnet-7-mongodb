using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using JwtDotNet7.Models.DTOs;
using JwtDotNet7.Models.Results;
using JwtDotNet7.Services.Interfaces;

namespace JwtDotNet7.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // validation...
            var user = await _authService.Register(registerDto);

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<LoginResult> Login([FromBody] LoginDto loginDto)
        {
            var result = _authService.Login(loginDto);
            if (result.Success != true)
            {
                return BadRequest(result);
            }

            HttpContext.Response.Cookies.Append("token", result.Token!, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1)
            });
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult<string> Logout()
        {
            HttpContext.Response.Cookies.Delete("token");
            return Ok(new
            {
                message = "Logged out successfully."
            });
        }
    }
}