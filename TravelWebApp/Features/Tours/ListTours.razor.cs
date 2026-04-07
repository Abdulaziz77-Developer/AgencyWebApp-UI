using Microsoft.AspNetCore.Components;
using TravelWebApp.Infrastrcuture.api.dtos.Tours;

namespace TravelWebApp.Features.Tours
{
    public partial class ListTours : ComponentBase
    {
        private List<TourDto> tours = new List<TourDto>();
        [Parameter] public string? SearchTerm { get; set; }
        protected override async Task OnInitializedAsync()
        {
            tours = await TourService.GetToursAsync();
        }
    }
}