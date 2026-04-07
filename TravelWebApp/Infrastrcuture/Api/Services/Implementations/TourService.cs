using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Models;
using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services
{
    public class TourService : ITourService
    {
        private readonly HttpClient _httpClient;

        public TourService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        // GET: api/tours
        public async Task<List<Tour>> GetToursAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Tour>>("api/tours") ?? new List<Tour>();
        }

        // GET: api/tours/{id}
        public async Task<Tour?> GetTourByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tour>($"api/tours/{id}");
        }

        // POST: api/tours
        public async Task<bool> CreateTourAsync(Tour tour)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tours", tour);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/tours/{id}
        public async Task<bool> UpdateTourAsync(int id, Tour tour)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tours/{id}", tour);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/tours/{id}
        public async Task<bool> DeleteTourAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/tours/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}