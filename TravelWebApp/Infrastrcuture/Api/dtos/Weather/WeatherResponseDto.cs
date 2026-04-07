using TravelWebApp.Infrastrcuture.api.dtos.Weather;

namespace TravelWebApp.Infrastrcuture.api.dtos.Weather
{
    public class WeatherResponseDto
    {
        public MainDataDto
         Main { get; set; }
        public WeatherDetailDto[] Weather { get; set; }
        public string Name { get; set; }
    }
}
