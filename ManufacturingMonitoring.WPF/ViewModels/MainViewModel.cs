using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ManufacturingMonitoring.WPF.Models;
using ManufacturingMonitoring.WPF.Services;

namespace ManufacturingMonitoring.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IAdoNetMetricRepository _repository;
        private string _statusMessage = "Ready";
        private int _totalNodesCount;
        private int _healthyNodesCount;
        private int _warningNodesCount;

        public ObservableCollection<NodeMetric> NodeMetrics { get; set; } = new();

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public int TotalNodesCount
        {
            get => _totalNodesCount;
            set { _totalNodesCount = value; OnPropertyChanged(); }
        }

        public int HealthyNodesCount
        {
            get => _healthyNodesCount;
            set { _healthyNodesCount = value; OnPropertyChanged(); }
        }

        public int WarningNodesCount
        {
            get => _warningNodesCount;
            set { _warningNodesCount = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            _repository = new AdoNetMetricRepository("Server=localhost;Database=MfgMonitoringDb;Trusted_Connection=True;TrustServerCertificate=True;");
            _ = RefreshMetricsAsync();
        }

        public async Task RefreshMetricsAsync()
        {
            StatusMessage = "Loading telemetry metrics via ADO.NET...";
            var metrics = await _repository.GetLatestNodeMetricsAsync();

            NodeMetrics.Clear();
            int healthy = 0, warning = 0;

            foreach (var item in metrics)
            {
                NodeMetrics.Add(item);
                if (item.Status == "Healthy") healthy++;
                else warning++;
            }

            TotalNodesCount = metrics.Count;
            HealthyNodesCount = healthy;
            WarningNodesCount = warning;
            StatusMessage = $"Last Updated: {DateTime.Now:HH:mm:ss} | {metrics.Count} Nodes Active";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
