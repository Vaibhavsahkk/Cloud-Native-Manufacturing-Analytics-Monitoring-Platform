using ManufacturingMonitoring.API.Data;
using ManufacturingMonitoring.API.Models.Mongo;
using MongoDB.Driver;

namespace ManufacturingMonitoring.API.Services
{
    public class MetricService : IMetricService
    {
        private readonly IMongoCollection<MetricDocument> _metrics;

        public MetricService(MongoDbContext context)
        {
            _metrics = context.Database.GetCollection<MetricDocument>("Metrics");
        }

        public async Task CreateMetricAsync(MetricDocument metric)
        {
            await _metrics.InsertOneAsync(metric);
        }

        public async Task<MetricDocument?> GetLatestMetricAsync(string serviceName)
        {
            return await _metrics
                .Find(m => m.ServiceName == serviceName)
                .SortByDescending(m => m.Timestamp)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MetricDocument>> GetMetricsByTimeRangeAsync(
            string serviceName,
            DateTime from,
            DateTime to)
        {
            return await _metrics
                .Find(m =>
                    m.ServiceName == serviceName &&
                    m.Timestamp >= from &&
                    m.Timestamp <= to)
                .SortBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<MetricBaselineResult> CalculateBaselineAsync(
            string serviceName,
            DateTime from,
            DateTime to)
        {
            var metrics = await _metrics
                .Find(m =>
                    m.ServiceName == serviceName &&
                    m.Timestamp >= from &&
                    m.Timestamp <= to)
                .ToListAsync();

            if (!metrics.Any())
                return new MetricBaselineResult
                {
                    ServiceName = serviceName,
                    From = from,
                    To = to
                };

            return new MetricBaselineResult
            {
                ServiceName = serviceName,
                From = from,
                To = to,
                AverageCpuUsage = metrics.Average(m => m.CpuUsage),
                AverageMemoryUsage = metrics.Average(m => m.MemoryUsage),
                AverageResponseTime = metrics.Average(m => m.ResponseTime),
                AverageErrorCount = metrics.Average(m => m.ErrorCount)
            };
        }
    }
}
