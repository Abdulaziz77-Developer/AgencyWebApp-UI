using TravelWebApp.Infrastrcuture.api.dtos.Hotels;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;


namespace TravelWebApp.Infrastrcuture.api.Services.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly HttpClient _httpClient;

        public HotelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/hotels
        public async Task<List<HotelDto>> GetHotelsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<HotelDto>>("api/hotels") ?? new List<HotelDto>();
        }

        // GET: api/hotels/{id}
        public async Task<HotelDto?> GetHotelByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<HotelDto>($"api/hotels/{id}");
        }

        // POST: api/hotels
        public async Task<bool> CreateHotelAsync(HotelDto hotel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/hotels", hotel);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/hotels/{id}
        public async Task<bool> UpdateHotelAsync(int id, HotelDto hotel)
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