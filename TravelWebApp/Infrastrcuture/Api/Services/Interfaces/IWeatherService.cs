using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse?> GetWeatherByCityAsync(string city);
    }
}
