using Microsoft.AspNetCore.Components;
using TravelWebApp.Infrastrcuture.api.dtos.Hotels;

namespace TravelWebApp.Features.Hotels
{
    public partial class ListHotels : ComponentBase
    {
        private List<HotelDto> hotels = new List<HotelDto>();
    [Parameter] public string? SearchTerm { get; set; } 


    protected override async Task OnInitializedAsync()
    {
        
        hotels = await HotelService.GetHotelsAsync();
    }
    }
}