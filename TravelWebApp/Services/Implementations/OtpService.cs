using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services.Implementations
{
    public class OtpService : IOtpService
    {
        private readonly HttpClient _http;

        public OtpService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> SendOtpAsync(string email)
        {
            // Отправляем запрос на твой контроллер
            var response = await _http.PostAsJsonAsync("api/Otp/send-otp", new { Email = email });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VerifyOtpAsync(string email, string code)
        {
            // Проверяем код
            var response = await _http.PostAsJsonAsync("api/Otp/verify-otp", new { Email = email, Code = code });
            return response.IsSuccessStatusCode;
        }
    }
}
