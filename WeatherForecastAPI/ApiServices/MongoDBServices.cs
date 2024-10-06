using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using WeatherForecastAPI.ConfigurationClasses;

namespace WeatherForecastAPI.ApiServices
{
    public class MongoDBServices
    {
        private readonly IMongoCollection<WeatherDetails> _collection;

        public MongoDBServices(IOptions<MongoDBclass> mongoDBClass)
        {
            var client = new MongoClient(mongoDBClass.Value.ConnectionString);
            var database = client.GetDatabase(mongoDBClass.Value.DatabaseName);
            _collection = database.GetCollection<WeatherDetails>(mongoDBClass.Value.CollectionName);
        }

        public async Task SaveWeatherDetailsAsync(WeatherDetails details)
        {
            await _collection.InsertOneAsync(details); 
        }

        public async Task<WeatherDetails> GetWeatherDetailsByIdAsync(string id)
        {
            return await _collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<List<WeatherDetails>> GetAllWeatherDetailsAsync()
        {
            return await _collection.AsQueryable().ToListAsync();        
        }

    }
}
