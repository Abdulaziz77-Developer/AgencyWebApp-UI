using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Infrastrcuture.api.dtos.Tours;
using TravelWebApp.Models;

namespace TravelWebApp.Infrastrcuture.api.Services.Interfaces
{
    public interface ITourService
    {
        Task<List<TourDto>> GetToursAsync();
        Task<TourDto?> GetTourByIdAsync(int id);
        Task<bool> CreateTourAsync(TourDto tour);
        Task<bool> UpdateTourAsync(int id, TourDto tour);
        Task<bool> DeleteTourAsync(int id);
    }
}