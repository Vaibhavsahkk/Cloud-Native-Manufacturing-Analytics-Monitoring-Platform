using MongoDB.Driver;
using ManufacturingMonitoring.API.Configurations;
using Microsoft.Extensions.Options;

namespace ManufacturingMonitoring.API.Data
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            Database = client.GetDatabase(settings.Value.DatabaseName);
        }
    }
}
