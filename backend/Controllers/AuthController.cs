using Microsoft.AspNetCore.Mvc;
using Lab.Api.Application.Services;
using Lab.Api.Application.DTOs;

namespace Lab.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _users;

        public AuthController(IUserService users)
        {
            _users = users;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var id = await _users.RegisterAsync(request);
                return CreatedAtAction(nameof(Register), new { id }, null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var auth = await _users.AuthenticateAsync(request);
            if (auth == null) return Unauthorized(new { message = "Invalid credentials" });
            return Ok(auth);
        }
    }
}
