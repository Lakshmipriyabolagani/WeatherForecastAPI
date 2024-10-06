using Microsoft.AspNetCore.Diagnostics;

namespace WeatherForecastAPI.WeatherApiExceptions
{
    public class WeatherExceptionHandler(ILogger<WeatherExceptionHandler> logger) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not WeatherException ex)
            {
                return ValueTask.FromResult(false);
            }
            logger.LogError("Error while retrieving data!!!");
            return ValueTask.FromResult(true);
        }
    }
}
