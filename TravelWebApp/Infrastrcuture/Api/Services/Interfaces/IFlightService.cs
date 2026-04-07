using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IFlightService
    {
        Task<List<FlightDto>> GetFlightsAsync();
        Task<FlightDto?> GetFlightByIdAsync(int id);
        Task<bool> CreateFlightAsync(FlightDto flight);
        Task<bool> UpdateFlightAsync(int id, FlightDto flight);
        Task<bool> DeleteFlightAsync(int id);
    }
}