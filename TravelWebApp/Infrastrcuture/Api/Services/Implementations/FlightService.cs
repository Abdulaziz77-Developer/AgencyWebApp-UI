using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;


namespace TravelWebApp.Infrastrcuture.api.Services.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly HttpClient _httpClient;

        public FlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/flights
        public async Task<List<FlightDto>> GetFlightsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<FlightDto>>("api/flights") ?? new List<FlightDto>();
        }

        // GET: api/flights/{id}
        public async Task<FlightDto?> GetFlightByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<FlightDto>($"api/flights/{id}");
        }

        // POST: api/flights
        public async Task<bool> CreateFlightAsync(FlightDto flight)
        {
            var response = await _httpClient.PostAsJsonAsync("api/flights", flight);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/flights/{id}
        public async Task<bool> UpdateFlightAsync(int id, FlightDto flight)
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