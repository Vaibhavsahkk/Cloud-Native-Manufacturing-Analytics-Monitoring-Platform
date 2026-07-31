using ManufacturingMonitoring.API.Models.Mongo;

namespace ManufacturingMonitoring.API.Services
{
    public interface IMetricService
    {
        Task CreateMetricAsync(MetricDocument metric);
        Task<MetricDocument?> GetLatestMetricAsync(string serviceName);
        Task<List<MetricDocument>> GetMetricsByTimeRangeAsync(
            string serviceName,
            DateTime from,
            DateTime to);
        Task<MetricBaselineResult> CalculateBaselineAsync(
            string serviceName,
            DateTime from,
            DateTime to);
    }
}
