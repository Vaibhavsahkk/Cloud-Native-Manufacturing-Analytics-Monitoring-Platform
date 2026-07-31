namespace ManufacturingMonitoring.API.Models.Mongo
{
    public class MetricBaselineResult
    {
        public string ServiceName { get; set; } = string.Empty;
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public double AverageCpuUsage { get; set; }
        public double AverageMemoryUsage { get; set; }
        public double AverageResponseTime { get; set; }
        public double AverageErrorCount { get; set; }
    }
}
