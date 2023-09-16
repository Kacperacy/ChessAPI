using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Chess.Infrastructure
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Test> _testCollection;
        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _testCollection = database.GetCollection<Test>(mongoDBSettings.Value.CollectionName);
        }

        public async Task Add(Test test)
        {
            await _testCollection.InsertOneAsync(test);
            return;
        }
    }
}
