using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ImageUploadApp.Models
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["ConnectionStrings:MongoDb"]);
            _database = client.GetDatabase(configuration["DatabaseName"]);
        }

        public IMongoDatabase Database => _database;
    }
}
