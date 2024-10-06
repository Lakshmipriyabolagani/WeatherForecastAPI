using WeatherForecastAPI.ConfigurationClasses;
using Microsoft.Extensions.Options;
using System.Text.Json;
namespace WeatherForecastAPI.ApiServices
{

    public class WeatherAPIServices
    {
        public HttpClient? httpClient;
        public WeatherApiConfigDetails? weatherApiConfigDetails;

        public WeatherAPIServices(HttpClient? _httpClient, IOptions<WeatherApiConfigDetails>? _weatherApiConfigDetails)
        {
            httpClient = _httpClient;
            weatherApiConfigDetails = _weatherApiConfigDetails?.Value;
        }

        public async Task<WeatherDetails> GetWeatherDetailsAsync(string city)
        {
            var url = $"{weatherApiConfigDetails?.baseUrl}?q={city}&appid={weatherApiConfigDetails?.ApiKey}&units=metrics";
            var response = await httpClient.GetStringAsync(url);
            using var jsonDocument = JsonDocument.Parse(response);
            var weatherData = JsonSerializer.Deserialize<JsonElement>(response);
            return new WeatherDetails
            {
                City = city,
                Description = weatherData.GetProperty("weather")[0].GetProperty("description").GetString(),
                Temperature = weatherData.GetProperty("main").GetProperty("temp").GetDouble(),
                Humidity = weatherData.GetProperty("main").GetProperty("humidity").GetInt32(),
                Wind = weatherData.GetProperty("wind").GetProperty("speed").GetDouble(),
                Precipitation = weatherData.TryGetProperty("rain", out var rain) ? rain.GetProperty("1h").GetDouble() : 0,
                DateTime = DateTime.Now
            };

        }
        

    }
}
