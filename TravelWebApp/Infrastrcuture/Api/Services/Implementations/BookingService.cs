using TravelWebApp.Models;
using TravelWebApp.Services.Interfaces;
using TravelWebApp.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using AgencyWebApp.Client.Infrastructure.Api.Dtos.Bookings;

namespace TravelWebApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthStateProvider _authProvider;

        // Конструктор для DI
        public BookingService(HttpClient httpClient, AuthenticationStateProvider authProvider)
        {
            _httpClient = httpClient;
            _authProvider = (CustomAuthStateProvider)authProvider;
        }

        // GET: api/bookings
        public async Task<List<BookingDto>> GetBookingsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<BookingDto>>("api/bookings") ?? new List<BookingDto>();
        }

        // GET: api/bookings/{id}
        public async Task<BookingDto?> GetBookingByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookingDto>($"api/bookings/{id}");
        }

        // POST: api/bookings
        public async Task<bool> CreateBookingAsync(BookingDto booking)
        {
            booking.Status = false;
            var response = await _httpClient.PostAsJsonAsync("api/bookings", booking);
            return response.IsSuccessStatusCode;
        }

        // PUT: api/bookings/{id}
        public async Task<bool> UpdateBookingAsync(int id, BookingDto booking)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/bookings/{id}", booking);
            return response.IsSuccessStatusCode;
        }

        // DELETE: api/bookings/{id}
        public async Task<bool> DeleteBookingAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/bookings/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
