using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Models;
using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/hotels
        public async Task<List<Hotel>> GetHotelsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Hotel>>("api/hotels") ?? new List<Hotel>();
        }

        // GET: api/hotels/{id}
        public async Task<Hotel?> GetHotelByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Hotel>($"api/hotels/{id}");
        }

        // POST: api/hotels
        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/hotels", hotel);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/hotels/{id}
        public async Task<bool> UpdateHotelAsync(int id, Hotel hotel)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/hotels/{id}", hotel);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/hotels/{id}
        public async Task<bool> DeleteHotelAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/hotels/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}