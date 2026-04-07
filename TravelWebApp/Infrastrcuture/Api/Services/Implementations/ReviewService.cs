using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Infrastrcuture.api.dtos.Reviews;
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

        public async Task<List<ReviewDto>> GetReviewsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReviewDto>>("api/Reviews") ?? new List<ReviewDto>();
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Reviews/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}