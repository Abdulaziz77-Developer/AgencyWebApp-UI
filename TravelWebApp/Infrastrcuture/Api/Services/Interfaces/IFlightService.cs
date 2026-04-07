using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IFlightService
    {
        Task<List<Flight>> GetFlightsAsync();
        Task<Flight?> GetFlightByIdAsync(int id);
        Task<bool> CreateFlightAsync(Flight flight);
        Task<bool> UpdateFlightAsync(int id, Flight flight);
        Task<bool> DeleteFlightAsync(int id);
    }
}