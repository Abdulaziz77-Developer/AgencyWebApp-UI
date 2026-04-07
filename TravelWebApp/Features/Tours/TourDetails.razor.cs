using Microsoft.AspNetCore.Components;
using TravelWebApp.Infrastrcuture.api.dtos.Tours;

namespace TravelWebApp.Features.Tours
{
    public partial class TourDetails : ComponentBase
    {
          [Parameter] public int Id { get; set; }
    private TourDto? tour;
    private bool isLoading = true;
    protected override async Task OnParametersSetAsync()
    {
        isLoading = true;
        try
        {
            Console.WriteLine("Id Start => " + Id);
            tour = await TourService.GetTourByIdAsync(Id);
            Console.WriteLine("Id End => " + tour?.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading tour: " + ex.Message);
            tour = null;
        }
        finally
        {
            isLoading = false;
        }
    }
    }
}