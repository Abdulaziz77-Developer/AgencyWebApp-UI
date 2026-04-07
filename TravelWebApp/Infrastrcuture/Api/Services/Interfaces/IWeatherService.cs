using TravelWebApp.Infrastrcuture.api.dtos.Weather;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponseDto?> GetWeatherByCityAsync(string city);
    }
}
