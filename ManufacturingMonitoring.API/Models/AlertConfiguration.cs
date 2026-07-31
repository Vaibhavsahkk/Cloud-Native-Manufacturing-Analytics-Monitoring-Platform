namespace ManufacturingMonitoring.API.Models
{
    public class AlertConfiguration
    {
        public int Id { get; set; }
        public string MetricType { get; set; } = string.Empty;
        public double ThresholdValue { get; set; }
        public string Severity { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
