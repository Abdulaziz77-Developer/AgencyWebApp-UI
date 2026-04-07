using AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights;
using Microsoft.AspNetCore.Components;

namespace TravelWebApp.Features.Flights
{
    public partial class FlightsPage : ComponentBase
    {
         private List<FlightDto> flights = new();
    
        protected override async Task OnInitializedAsync() 
        {
            flights = await FlightService.GetFlightsAsync();
        }
    }
}