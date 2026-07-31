using ManufacturingMonitoring.API.DTOs;

namespace ManufacturingMonitoring.API.Services
{
    public interface IAlertService
    {
        Task<AlertConfigResponseDto?> CreateAlertConfig(AlertConfigRequestDto request);
        Task<List<AlertConfigResponseDto>> GetAlertConfigs();
    }
}
