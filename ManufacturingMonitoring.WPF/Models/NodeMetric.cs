using System;

namespace ManufacturingMonitoring.WPF.Models
{
    public class NodeMetric
    {
        public int Id { get; set; }
        public string NodeId { get; set; } = string.Empty;
        public string NodeName { get; set; } = string.Empty;
        public double CpuUsagePercent { get; set; }
        public double MemoryUsageMb { get; set; }
        public double TemperatureCelsius { get; set; }
        public string Status { get; set; } = "Healthy";
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
