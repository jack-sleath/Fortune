using Fortune.Models.Configs;
using Fortune.Repositories.MongoDB.MappedSaveObject;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Fortune.Repositories.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> options)
        {
            var mongoClientSettings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);

            var mongoClient = new MongoClient(mongoClientSettings);
            
            _database = mongoClient.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<MongoDbFortuneModel> Fortunes => _database.GetCollection<MongoDbFortuneModel>("Fortunes");
    }
}
