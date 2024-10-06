using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherForecastAPI
{
    public class WeatherDetails
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
        public double? Temperature { get; set; }
        public double? Humidity { get; set; }
        public double? Wind { get; set; }
        public double? Precipitation { get; set; }
        public DateTime DateTime { get; set; }

    }
}
