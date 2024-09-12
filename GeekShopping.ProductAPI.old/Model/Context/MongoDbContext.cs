using MongoDB.Driver;
using GeekShopping.ProductAPI.Data.ValueObjects;

namespace GeekShopping.ProductAPI.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<ImageVO> Images => _database.GetCollection<ImageVO>("images");
    }
}
