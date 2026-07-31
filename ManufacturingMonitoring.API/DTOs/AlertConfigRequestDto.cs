using System.ComponentModel.DataAnnotations;

namespace ManufacturingMonitoring.API.DTOs
{
    public class AlertConfigRequestDto
    {
        [Required(ErrorMessage = "MetricType is required")]
        public string MetricType { get; set; } = string.Empty;

        [Required(ErrorMessage = "ThresholdValue is required")]
        [Range(0, 100, ErrorMessage = "ThresholdValue must be between 0 and 100")]
        public double ThresholdValue { get; set; }

        [Required(ErrorMessage = "Severity is required")]
        public string Severity { get; set; } = string.Empty;
    }
}
