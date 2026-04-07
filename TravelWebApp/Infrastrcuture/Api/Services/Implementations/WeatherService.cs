using System.Net.Http;
using TravelWebApp.Infrastrcuture.api.dtos.Weather;
using TravelWebApp.Models;
using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "eeb3c6c56b0a55b23e4550870d148226";
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<WeatherResponseDto?> GetWeatherByCityAsync(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=ru";
                return await _httpClient.GetFromJsonAsync<WeatherResponseDto>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Weather Error: {ex.Message}");
                return null;
            }
        }
    }
}
