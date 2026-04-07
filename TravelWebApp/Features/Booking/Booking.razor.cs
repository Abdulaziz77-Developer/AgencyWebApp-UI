using AgencyWebApp.Client.Infrastructure.Api.Dtos.Bookings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using TravelWebApp.Infrastrcuture.api.auth;
using TravelWebApp.Infrastrcuture.api.dtos.Hotels;
using TravelWebApp.Infrastrcuture.api.dtos.Tours;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;

namespace TravelWebApp.Features.Booking
{
    public partial class Booking
    {
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IHotelService HotelService { get; set; }
        [Inject] public ITourService TourService { get; set; }
        [Inject] public IFlightService FlightService { get; set; }
        [Inject] public IWeatherService WeatherService { get; set; }
        [Inject] public IOtpService OtpService { get; set; }
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject] public IBookingService BookingService { get; set; }
        private bool showOtpModal = false;
        private bool isProcessing = false;
        private string enteredOtp = "";
        private string otpError = "";
        private string userEmail = "";
        private int selectedEntityId = 0;
        private string bookingType = "Tour";

    private TourDto? tour;
    private HotelDto? hotel;
    private AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights.FlightDto? flight;
    private Infrastrcuture.api.dtos.Weather.WeatherResponseDto? weather;

    // Генерируем URL иконки в коде, чтобы избежать проблем с Razor синтаксисом
    private string WeatherIconUrl => (weather != null && weather.Weather.Length > 0)
        ? $"https://openweathermap.org/img/wn/{weather.Weather[0].Icon}@4x.png"
        : "";

        protected override async Task OnInitializedAsync()
        {
            var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query);

            if (query.TryGetValue("type", out var typeVals) && !string.IsNullOrEmpty(typeVals.FirstOrDefault()))
                bookingType = typeVals.First();

            if (query.TryGetValue("id", out var idVals) && int.TryParse(idVals.FirstOrDefault(), out var parsedId))
                selectedEntityId = parsedId;

            // Use selectedEntityId (parsed above) for service calls to avoid referencing undefined local variables
            switch (bookingType)
            {
                case "Hotel":
                    if (selectedEntityId != 0)
                    {
                        hotel = await HotelService.GetHotelByIdAsync(selectedEntityId);
                        if (hotel != null) await LoadWeatherData(hotel.City);
                    }
                    break;
                case "Tour":
                    if (selectedEntityId != 0)
                    {
                        tour = await TourService.GetTourByIdAsync(selectedEntityId);
                        if (tour != null) await LoadWeatherData(tour.Region);
                    }
                    break;
                case "Flight":
                    if (selectedEntityId != 0)
                        flight = await FlightService.GetFlightByIdAsync(selectedEntityId);
                    break;
            }
        }

    private async Task LoadWeatherData(string city)
    {
        if (!string.IsNullOrEmpty(city))
        {
            weather = await WeatherService.GetWeatherByCityAsync(city);
            StateHasChanged();
        }
    }

    private async Task StartOtpProcess()
    {
        isProcessing = true;
        otpError = "";
        var customAuthProvider = (CustomAuthStateProvider)AuthStateProvider;
        userEmail = await customAuthProvider.GetUserEmail();

        if (string.IsNullOrEmpty(userEmail))
        {
            isProcessing = false;
            Navigation.NavigateTo($"/login?ReturnUrl={Uri.EscapeDataString(Navigation.Uri)}");
            return;
        }

        var sent = await OtpService.SendOtpAsync(userEmail);
        if (sent) showOtpModal = true;
        else otpError = "Ошибка отправки кода.";
        isProcessing = false;
    }

    private async Task VerifyAndFinalize()
    {
        var isVerified = await OtpService.VerifyOtpAsync(userEmail, enteredOtp);
        if (isVerified)
        {
            await HandleBooking();
            showOtpModal = false;
        }
        else otpError = "Неверный код.";
    }

    private async Task HandleBooking()
    {
        var customAuthProvider = (CustomAuthStateProvider)AuthStateProvider;
        var userId = customAuthProvider.GetUserId();
        if (userId == null || selectedEntityId == 0) return;

        var booking = new BookingDto
        {
            UserId = userId.Value,
            BookingDate = DateTime.Now,
            Status = true,
            TotalPrice = 0
        };

        if (bookingType == "Tour") booking.TourId = selectedEntityId;
        else if (bookingType == "Hotel") booking.HotelId = selectedEntityId;
        else if (bookingType == "Flight") booking.FlightId = selectedEntityId;

        if (await BookingService.CreateBookingAsync(booking))
        {
            Navigation.NavigateTo("/");
        }
    }
    }
}