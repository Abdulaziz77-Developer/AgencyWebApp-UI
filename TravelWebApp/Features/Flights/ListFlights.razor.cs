using AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights;
using Microsoft.AspNetCore.Components;

namespace TravelWebApp.Features.Flights
{
    public partial class ListFlights : ComponentBase
    {
        public List<FlightDto> flights = new();

        [Parameter] public string? SearchTerm { get; set; }
        [Parameter] public DateTime? Date { get; set; }

        protected override async Task OnInitializedAsync()
        {
            flights = await FlightService.GetFlightsAsync();
        }
    }
}