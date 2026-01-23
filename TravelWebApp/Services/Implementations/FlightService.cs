using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TravelWebApp.Models;
using TravelWebApp.Services.Interfaces;

namespace TravelWebApp.Services
{
    public class FlightService : IFlightService
    {
        private readonly HttpClient _httpClient;

        public FlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/flights
        public async Task<List<Flight>> GetFlightsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Flight>>("api/flights") ?? new List<Flight>();
        }

        // GET: api/flights/{id}
        public async Task<Flight?> GetFlightByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Flight>($"api/flights/{id}");
        }

        // POST: api/flights
        public async Task<bool> CreateFlightAsync(Flight flight)
        {
            var response = await _httpClient.PostAsJsonAsync("api/flights", flight);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/flights/{id}
        public async Task<bool> UpdateFlightAsync(int id, Flight flight)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/flights/{id}", flight);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/flights/{id}
        public async Task<bool> DeleteFlightAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/flights/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}