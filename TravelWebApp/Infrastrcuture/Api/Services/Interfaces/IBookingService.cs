using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyWebApp.Client.Infrastructure.Api.Dtos.Bookings;

namespace TravelWebApp.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetBookingsAsync();
        Task<BookingDto?> GetBookingByIdAsync(int id);
        Task<bool> CreateBookingAsync(BookingDto booking);
        Task<bool> UpdateBookingAsync(int id, BookingDto booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}