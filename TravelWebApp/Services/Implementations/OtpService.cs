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
            var response = await _http.PostAsJsonAsync("api/Otp/send-otp", new { Email = email });
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VerifyOtpAsync(string email, string code)
        {
            var response = await _http.PostAsJsonAsync("api/Otp/verify-otp", new { Email = email, Code = code });
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> SendConfirmationNotificationAsync(string email, int bookingId)
        {
            var response = await _http.PostAsync($"api/Email/confirm-notification?email={email}&id={bookingId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendRejectionNotificationAsync(string email, int bookingId)
        {
            var response = await _http.PostAsync($"api/Email/reject-notification?email={email}&id={bookingId}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
