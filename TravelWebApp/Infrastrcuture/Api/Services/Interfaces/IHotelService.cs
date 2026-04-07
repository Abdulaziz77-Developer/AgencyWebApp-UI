using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Infrastrcuture.api.dtos.Hotels;
using TravelWebApp.Models;

namespace TravelWebApp.Infrastrcuture.api.Services.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelDto>> GetHotelsAsync();
        Task<HotelDto?> GetHotelByIdAsync(int id);
        Task<bool> CreateHotelAsync(HotelDto hotel);
        Task<bool> UpdateHotelAsync(int id, HotelDto hotel);
        Task<bool> DeleteHotelAsync(int id);
    }
}