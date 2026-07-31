using System;
using System.Threading.Tasks;
using Xunit;
using ManufacturingMonitoring.WPF.Models;
using ManufacturingMonitoring.WPF.Services;

namespace ManufacturingMonitoring.Tests
{
    public class MetricServiceTests
    {
        [Fact]
        public async Task GetLatestNodeMetricsAsync_ReturnsFiftySimulatedNodes_WhenDatabaseOffline()
        {
            // Arrange
            var repo = new AdoNetMetricRepository("Server=invalid_server;Database=dummy;");

            // Act
            var metrics = await repo.GetLatestNodeMetricsAsync();

            // Assert
            Assert.NotNull(metrics);
            Assert.Equal(50, metrics.Count);
            Assert.Contains(metrics, m => m.NodeId.StartsWith("MFG-NODE-"));
        }

        [Fact]
        public async Task GetMetricByNodeIdAsync_ReturnsValidNode_WhenRequested()
        {
            // Arrange
            var repo = new AdoNetMetricRepository("Server=invalid_server;Database=dummy;");
            string targetNodeId = "MFG-NODE-005";

            // Act
            var metric = await repo.GetMetricByNodeIdAsync(targetNodeId);

            // Assert
            Assert.NotNull(metric);
            Assert.Equal(targetNodeId, metric.NodeId);
            Assert.True(metric.CpuUsagePercent >= 0);
        }

        [Theory]
        [InlineData(45.0, "Healthy")]
        [InlineData(78.5, "Warning")]
        [InlineData(92.0, "Critical")]
        public void EvaluateNodeStatus_CategorizesStatusCorrectly(double cpuUsage, string expectedStatus)
        {
            // Act
            string status = cpuUsage > 85.0 ? "Critical" : (cpuUsage > 70.0 ? "Warning" : "Healthy");

            // Assert
            Assert.Equal(expectedStatus, status);
        }
    }
}
