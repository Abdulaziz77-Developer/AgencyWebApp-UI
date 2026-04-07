using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;     // Для кастомного провайдера
using System.Security.Claims;
using TravelWebApp.Infrastrcuture.api.auth;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;

namespace AgencyWebApp.Client.Features.User
{
    public partial class UserDashboard : ComponentBase
    {
        [Inject] public IBookingService BookingService { get; set; } = default!;
        [Inject] public ITourService TourService { get; set; } = default!;
        [Inject] public IHotelService HotelService { get; set; } = default!;
        [Inject] public IFlightService FlightService { get; set; } = default!;
        [Inject] public AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;

        private List<UserBookingViewModel> userBookings = new();
        public bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            try
            {
                var authState = await AuthStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                // Используем ClaimTypes.NameIdentifier (это стандарт для "nameid")
                var userIdClaim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    isLoading = false;
                    return;
                }

                var allBookings = await BookingService.GetBookingsAsync();
                // Фильтруем бронирования именно этого пользователя
                var currentUserBookings = allBookings.Where(b => b.UserId == userId).ToList();

                userBookings.Clear(); // Очищаем список перед заполнением

                foreach (var booking in currentUserBookings)
                {
                    var bookingViewModel = new UserBookingViewModel
                    {
                        Id = booking.Id,
                        BookedDate = booking.BookingDate.ToString("yyyy-MM-dd"),
                        TripDate = booking.BookingDate.ToString("yyyy-MM-dd"), 
                        Status = booking.Status,
                        Price = Random.Shared.Next(250, 450)
                    };

                    // Загружаем детали (Тур, Отель или Рейс)
                    if (booking.TourId.HasValue)
                    {
                        var tour = await TourService.GetTourByIdAsync(booking.TourId.Value);
                        if (tour != null) bookingViewModel.Title = tour.Title;
                    }
                    else if (booking.HotelId.HasValue)
                    {
                        var hotel = await HotelService.GetHotelByIdAsync(booking.HotelId.Value);
                        if (hotel != null) bookingViewModel.Title = hotel.Name;
                    }
                    else if (booking.FlightId.HasValue)
                    {
                        var flight = await FlightService.GetFlightByIdAsync(booking.FlightId.Value);
                        if (flight != null) bookingViewModel.Title = $"{flight.airPlaneName} - {flight.flightNumber}";
                    }

                    userBookings.Add(bookingViewModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки Dashboard: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        public async Task Logout()
        {
            // Исправление: приводим к твоему кастомному провайдеру, чтобы вызвать метод разлогина
            if (AuthStateProvider is CustomAuthStateProvider customProvider)
            {
                await customProvider.MarkUserAsLoggedOut();
            }
            
            Navigation.NavigateTo("/", true);
        }

        // Вспомогательная модель для отображения
        public class UserBookingViewModel
        {
            public int Id { get; set; }
            public string Title { get; set; } = "Загрузка...";
            public string BookedDate { get; set; } = "";
            public bool Status { get; set; }
            public string TripDate { get; set; } = "";
            public int Guests { get; set; } = Random.Shared.Next(1, 3);
            public int Price { get; set; }
        }
    }
}