using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<bool> CreateBookingAsync(Booking booking);
        Task<bool> UpdateBookingAsync(int id, Booking booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}