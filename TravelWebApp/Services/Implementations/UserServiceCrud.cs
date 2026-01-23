using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Models;
using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services.Implementations
{
    public class UserServiceCrud : IUserServiceCrud
    {
        private readonly HttpClient _httpClient;

        public UserServiceCrud(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateUserAsync(User User)
        {
            // Note: The interface returns Task<User>, but typically POST returns the created entity or we just return the input if successful.
            // Assuming the API returns the created user.
            var response = await _httpClient.PostAsJsonAsync("api/Users", User);
            return response.IsSuccessStatusCode;
                
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<User>>("api/Users") ?? new List<User>();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"api/Users/{id}");
        }

        public async Task<bool> UpdateUserAsync(int id, User User)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Users/{id}", User);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Users/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}