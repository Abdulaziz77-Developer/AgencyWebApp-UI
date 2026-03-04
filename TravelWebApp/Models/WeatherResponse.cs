namespace TravelWebApp.Models
{
    public class WeatherResponse
    {
        public MainData Main { get; set; }
        public WeatherDetail[] Weather { get; set; }
        public string Name { get; set; }
    }
}
