using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Infrastrcuture.api.dtos.Users;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;

namespace TravelWebApp.Infrastrcuture.api.Services.Implementations
{
    public class UserServiceCrud : IUserServiceCrud
    {
        private readonly HttpClient _httpClient;

        public UserServiceCrud(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateUserAsync(UserDto User)
        {
            // Note: The interface returns Task<User>, but typically POST returns the created entity or we just return the input if successful.
            // Assuming the API returns the created user.
            var response = await _httpClient.PostAsJsonAsync("api/Users", User);
            return response.IsSuccessStatusCode;
                
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UserDto>>("api/Users") ?? new List<UserDto>();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UserDto>($"api/Users/{id}");
        }

        public async Task<bool> UpdateUserAsync(int id, UserDto User)
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