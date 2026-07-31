using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ManufacturingMonitoring.API.Models.Mongo
{
    public class MetricDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("serviceName")]
        public string ServiceName { get; set; } = string.Empty;

        [BsonElement("cpuUsage")]
        public double CpuUsage { get; set; }

        [BsonElement("memoryUsage")]
        public double MemoryUsage { get; set; }

        [BsonElement("responseTime")]
        public double ResponseTime { get; set; }

        [BsonElement("errorCount")]
        public int ErrorCount { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = string.Empty; // UP / DOWN
    }
}
