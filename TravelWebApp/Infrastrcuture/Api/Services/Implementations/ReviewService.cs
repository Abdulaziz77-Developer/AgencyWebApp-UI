using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Models;
using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReviewDTO>> GetReviewsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReviewDTO>>("api/Reviews") ?? new List<ReviewDTO>();
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Reviews/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}