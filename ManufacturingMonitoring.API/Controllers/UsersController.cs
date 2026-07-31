using ManufacturingMonitoring.API.DTOs;
using ManufacturingMonitoring.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturingMonitoring.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            var result = await _userService.CreateUser(request);

            if (result == null)
            {
                return BadRequest(new { message = "Invalid role specified" });
            }

            return CreatedAtAction(nameof(GetUsers), new { id = result.Id }, result);
        }
    }
}
