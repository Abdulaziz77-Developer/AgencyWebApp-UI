using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Infrastrcuture.api.dtos.Tours;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;

namespace TravelWebApp.Infrastrcuture.api.Services.Implementations
{
    public class TourService : ITourService
    {
        private readonly HttpClient _httpClient;

        public TourService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        // GET: api/tours
        public async Task<List<TourDto>> GetToursAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TourDto>>("api/tours") ?? new List<TourDto>();
        }

        // GET: api/tours/{id}
        public async Task<TourDto?> GetTourByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TourDto>($"api/tours/{id}");
        }

        // POST: api/tours
        public async Task<bool> CreateTourAsync(TourDto tour)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tours", tour);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/tours/{id}
        public async Task<bool> UpdateTourAsync(int id, TourDto tour)
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