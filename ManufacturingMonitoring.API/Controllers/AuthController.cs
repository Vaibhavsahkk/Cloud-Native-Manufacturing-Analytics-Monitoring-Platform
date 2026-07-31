using ManufacturingMonitoring.API.DTOs;
using ManufacturingMonitoring.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturingMonitoring.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _userService.ValidateLogin(request);

            if (result == null)
            {
                return BadRequest(new { message = "Invalid credentials" });
            }

            return Ok(result);
        }
    }
}
