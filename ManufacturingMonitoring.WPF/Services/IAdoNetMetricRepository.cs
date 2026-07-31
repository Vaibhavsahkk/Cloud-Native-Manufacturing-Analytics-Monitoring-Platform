using System.Collections.Generic;
using System.Threading.Tasks;
using ManufacturingMonitoring.WPF.Models;

namespace ManufacturingMonitoring.WPF.Services
{
    public interface IAdoNetMetricRepository
    {
        Task<List<NodeMetric>> GetLatestNodeMetricsAsync();
        Task<NodeMetric?> GetMetricByNodeIdAsync(string nodeId);
        Task<bool> SaveMetricAsync(NodeMetric metric);
    }
}
