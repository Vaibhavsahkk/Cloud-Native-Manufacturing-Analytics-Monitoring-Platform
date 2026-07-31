using ManufacturingMonitoring.API.Models;

namespace ManufacturingMonitoring.API.Data.Repositories
{
    public interface IAlertConfigRepository
    {
        Task<List<AlertConfiguration>> GetAllAsync();
        Task AddAsync(AlertConfiguration config);
    }
}
