using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AgencyWebApp.Client.Infrastructure.Api.Dtos.Bookings;
using AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using TravelWebApp.Infrastrcuture.api.dtos.Hotels;
using TravelWebApp.Infrastrcuture.api.dtos.Tours;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;

namespace AgencyWebApp.Client.Features.Booking
{
    public partial class BookingPage : ComponentBase
    {
        [Inject] public IBookingService BookingService { get; set; } = null!;
        [Inject] public ITourService TourService { get; set; } = null!;
        [Inject] public IHotelService HotelService { get; set; } = null!;
        [Inject] public IFlightService FlightService { get; set; } = null!;
        [Inject] public NavigationManager Navigation { get; set; } = null!;
        [Inject] public IHttpContextAccessor HttpContextAccessor { get; set; } = null!;
        private bool bookingSubmitted = false;
        private BookingRequest bookingModel = new BookingRequest();
        private string bookingType = "Tour";
        private int selectedEntityId = 0;

        private List<TourDto> tours = new();
        private List<HotelDto> hotels = new();
        private List<FlightDto> flights = new();

        protected override async Task OnInitializedAsync()
        {
            tours = await TourService.GetToursAsync();
            hotels = await HotelService.GetHotelsAsync();
            flights = await FlightService.GetFlightsAsync();

            // Параметры ReturnUrl
            var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query);
            if (query.TryGetValue("type", out var t) && !string.IsNullOrEmpty(t.FirstOrDefault()))
                bookingType = t.First();
            if (query.TryGetValue("id", out var idVals) && int.TryParse(idVals.FirstOrDefault(), out var parsed))
                selectedEntityId = parsed;
        }

        private async Task HandleBooking()
        {   
            // 🔹 Получаем UserId из Claims (Cookie Authentication)
            var user = HttpContextAccessor.HttpContext?.User;
            var userIdClaim = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                // Если не аутентифицирован, перенаправляем на login с ReturnUrl
                var returnUrl = $"/bookingpage?type={bookingType}&id={selectedEntityId}";
                Navigation.NavigateTo($"/login?ReturnUrl={Uri.EscapeDataString(returnUrl)}");
                return;
            }

            if (selectedEntityId == 0) return;

            BookingDto   booking = new BookingDto
            {
                UserId = userId,
                BookingDate = DateTime.Now
            };

            switch (bookingType)
            {
                case "Tour": booking.TourId = selectedEntityId; break;
                case "Hotel": booking.HotelId = selectedEntityId; break;
                case "Flight": booking.FlightId = selectedEntityId; break;
            }

            var success = await BookingService.CreateBookingAsync(booking);
            if (success) bookingSubmitted = true;
        }

         public class BookingRequest
        {
            public DateTime StartDate { get; set; } = DateTime.Today;
            public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
            [Range(1, 20, ErrorMessage = "Please enter between 1 and 20 guests")]
            public int Guests { get; set; } = 1;
        }
    }
}