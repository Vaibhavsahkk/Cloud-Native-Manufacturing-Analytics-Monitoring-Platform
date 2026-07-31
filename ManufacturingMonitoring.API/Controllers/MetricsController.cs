using ManufacturingMonitoring.API.Models.Mongo;
using ManufacturingMonitoring.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManufacturingMonitoring.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricService _metricService;

        public MetricsController(IMetricService metricService)
        {
            _metricService = metricService;
        }

        // POST: /api/metrics
        [HttpPost]
        public async Task<IActionResult> CreateMetric([FromBody] MetricDocument metric)
        {
            // Use provided timestamp if available, otherwise use current UTC time
            if (metric.Timestamp == default)
                metric.Timestamp = DateTime.UtcNow;
            
            await _metricService.CreateMetricAsync(metric);
            return CreatedAtAction(nameof(GetLatestMetric), new { serviceName = metric.ServiceName }, metric);
        }

        // GET: /api/metrics/latest?serviceName=ServiceA
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestMetric([FromQuery] string serviceName)
        {
            var metric = await _metricService.GetLatestMetricAsync(serviceName);

            if (metric == null)
                return NotFound();

            return Ok(metric);
        }

        // GET: /api/metrics/history?serviceName=ServiceA&from=2026-01-01&to=2026-01-23
        [HttpGet("history")]
        public async Task<IActionResult> GetMetricsHistory(
            [FromQuery] string serviceName,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            var metrics = await _metricService.GetMetricsByTimeRangeAsync(serviceName, from, to);
            return Ok(metrics);
        }

        // GET: /api/metrics/baseline?serviceName=ServiceA&from=2026-01-01T00:00:00Z&to=2026-01-23T23:59:59Z
        [HttpGet("baseline")]
        public async Task<IActionResult> GetBaseline(
            [FromQuery] string serviceName,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                return BadRequest("serviceName is required.");

            if (from >= to)
                return BadRequest("Invalid time range.");

            var baseline = await _metricService.CalculateBaselineAsync(
                serviceName, from, to);

            return Ok(baseline);
        }
    }
}
