using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.ApiServices;
using WeatherForecastAPI.WeatherApiExceptions;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherAPIServices _weatherApiServices;
        private readonly MongoDBServices _mongoDbServices;

        public WeatherForecastController(WeatherAPIServices weatherApiServices, MongoDBServices mongoDbServices)
        {
            _weatherApiServices = weatherApiServices;
            _mongoDbServices = mongoDbServices;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (city.GetType() == typeof(string))
            {
                throw new WeatherException("OOpsie! Numerical cities are yet to come into existence!");
            }
            var weatherDetails = await _weatherApiServices.GetWeatherDetailsAsync(city);
            await _mongoDbServices.SaveWeatherDetailsAsync(weatherDetails);
            return Ok(weatherDetails);
            //throw new WeatherException("Cannot retrieve data for the mentioned city!!");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetWeatherById(string id)
        {
            var weatherDetails = await _mongoDbServices.GetWeatherDetailsByIdAsync(id);
            if (weatherDetails == null)
            {
                throw new WeatherException("Data cannot be found with the given ID!!");
            }

            return Ok(weatherDetails);
        }    

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllWeatherDetails()
        {  
            var weatherDetails = await _mongoDbServices.GetAllWeatherDetailsAsync();
            return Ok(weatherDetails);
            throw new WeatherException("Cannot retrieve data!!!");
        }
    }
}
