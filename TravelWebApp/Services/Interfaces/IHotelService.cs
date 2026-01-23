using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetHotelsAsync();
        Task<Hotel?> GetHotelByIdAsync(int id);
        Task<bool> CreateHotelAsync(Hotel hotel);
        Task<bool> UpdateHotelAsync(int id, Hotel hotel);
        Task<bool> DeleteHotelAsync(int id);
    }
}