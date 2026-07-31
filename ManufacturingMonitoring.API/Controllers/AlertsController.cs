using ManufacturingMonitoring.API.DTOs;
using ManufacturingMonitoring.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturingMonitoring.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;

        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlerts()
        {
            var configs = await _alertService.GetAlertConfigs();
            return Ok(configs);
        }

        [HttpPost("config")]
        public async Task<IActionResult> CreateAlertConfig([FromBody] AlertConfigRequestDto request)
        {
            var result = await _alertService.CreateAlertConfig(request);

            if (result == null)
            {
                return BadRequest(new { message = "Failed to create alert configuration" });
            }

            return CreatedAtAction(nameof(GetAlerts), new { id = result.Id }, result);
        }
    }
}
