using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface ITourService
    {
        Task<List<Tour>> GetToursAsync();
        Task<Tour?> GetTourByIdAsync(int id);
        Task<bool> CreateTourAsync(Tour tour);
        Task<bool> UpdateTourAsync(int id, Tour tour);
        Task<bool> DeleteTourAsync(int id);
    }
}