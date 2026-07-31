using ManufacturingMonitoring.API.Data.Repositories;
using ManufacturingMonitoring.API.DTOs;
using ManufacturingMonitoring.API.Models;

namespace ManufacturingMonitoring.API.Services
{
    public class AlertService : IAlertService
    {
        private readonly IAlertConfigRepository _repository;

        public AlertService(IAlertConfigRepository repository)
        {
            _repository = repository;
        }

        public async Task<AlertConfigResponseDto?> CreateAlertConfig(AlertConfigRequestDto request)
        {
            var config = new AlertConfiguration
            {
                MetricType = request.MetricType,
                ThresholdValue = request.ThresholdValue,
                Severity = request.Severity,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(config);

            var response = new AlertConfigResponseDto
            {
                Id = config.Id,
                MetricType = config.MetricType,
                ThresholdValue = config.ThresholdValue,
                Severity = config.Severity
            };

            return response;
        }

        public async Task<List<AlertConfigResponseDto>> GetAlertConfigs()
        {
            var configs = await _repository.GetAllAsync();

            var configDtos = configs.Select(c => new AlertConfigResponseDto
            {
                Id = c.Id,
                MetricType = c.MetricType,
                ThresholdValue = c.ThresholdValue,
                Severity = c.Severity
            }).ToList();

            return configDtos;
        }
    }
}
