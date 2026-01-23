using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            // Sending login credentials to the API
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                // Return the response content (e.g., JWT token)
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            var registerData = new { Email = email, Password = password, ConfirmPassword = confirmPassword };
            // Sending registration data to the API
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerData);

            return response.IsSuccessStatusCode;
        }
    }
}