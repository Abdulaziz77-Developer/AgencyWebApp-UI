using AgencyWebApp.Client.Infrastructure.Api.Dtos.Bookings;
using AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using TravelWebApp.Infrastrcuture.api.dtos.Hotels;
using TravelWebApp.Infrastrcuture.api.dtos.Reviews;
using TravelWebApp.Infrastrcuture.api.dtos.Tours;
using TravelWebApp.Infrastrcuture.api.dtos.Users;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;

namespace TravelWebApp.Features.Admin
{
    public partial class AdminDashboard
    {
        [Inject] public ITourService TourService { get; set; } = default!;
        [Inject] public IHotelService HotelService { get; set; } = default!;
        [Inject] public IFlightService FlightService { get; set; } = default!;
        [Inject] public IBookingService BookingService { get; set; } = default!;
        [Inject] public IUserService UserServiceCrud { get; set; } = default!;
        [Inject] public IReviewService ReviewService { get; set; } = default!;
        [Inject] public IOtpService OtpService { get; set; } = default!;
        [Inject] public NavigationManager Navigation { get; set; } = default!;
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public IWebHostEnvironment Environment { get; set; } = default!;
        private List<TourDto> tours = new();
        private List<ReviewDto> adminReviews = new();
        private List<AdminReviewModel> reviews = new();
        private List<BookingDto> bookings = new();
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var isAdmin = user.IsInRole("Admin");
                if (!isAdmin)
                {
                    Navigation.NavigateTo("/login");
                    return;
                }

                // Загружаем все данные с API
                tours = await TourService.GetToursAsync() ?? new List<TourDto>();
                Console.WriteLine($"Туры загружены: {tours.Count}");

                adminHotels = await HotelService.GetHotelsAsync() ?? new List<HotelDto>();
                Console.WriteLine($"Отели загружены: {adminHotels.Count}");

                adminFlights = await FlightService.GetFlightsAsync() ?? new List<FlightDto>();
                Console.WriteLine($"Рейсы загружены: {adminFlights.Count}");

                var users = new List<UserDto>(); // TODO: Implement GetUsersAsync in IUserService or use appropriate method
                Console.WriteLine($"Пользователи загружены: {users.Count}");

                // Загружаем бронирования
                bookings = await BookingService.GetBookingsAsync();
                adminBookings = bookings.Select(b => new AdminBookingModel
                {
                    Id = b.Id,
                    User = users.FirstOrDefault(u => u.Id == b.UserId)?.FullName ?? "Неизвестный пользователь",
                    Date = b.BookingDate.ToShortDateString(),
                    Status = b.Status,
                    Price = b.TourId.HasValue ? tours.FirstOrDefault(t => t.Id == b.TourId)?.Price.ToString("C0") ?? "0 ₽" :
                           b.HotelId.HasValue ? adminHotels.FirstOrDefault(h => h.Id == b.HotelId)?.Price.ToString("C0") ?? "0 ₽" :
                           b.FlightId.HasValue ? adminFlights.FirstOrDefault(f => f.id == b.FlightId)?.Price.ToString("C0") ?? "0 ₽" :
                           b.TotalPrice.ToString("C0"),
                    Item = b.TourId.HasValue ? tours.FirstOrDefault(t => t.Id == b.TourId)?.Title ?? "Неизвестный тур" :
                           b.HotelId.HasValue ? adminHotels.FirstOrDefault(h => h.Id == b.HotelId)?.Name ?? "Неизвестный отель" :
                           b.FlightId.HasValue ? (adminFlights.FirstOrDefault(f => f.id == b.FlightId) is FlightDto fl ? $"{fl.airPlaneName} ({fl.fromCity} -> {fl.toCity})" : "Неизвестный рейс") :
                           "Неизвестно"
                }).ToList();
                adminReviews = await ReviewService.GetReviewsAsync() ?? new List<ReviewDto>();
                Console.WriteLine($"Бронирования загружены: {adminBookings.Count}");

                // Загружаем отзывы - ИСПРАВЛЕНО: используем CreatedAt вместо Date
                // adminReviews = await ReviewService.GetReviewsAsync() ?? new List<ReviewDTO>();
                Console.WriteLine($"Отзывы загружены: {adminReviews.Count}");

                reviews = adminReviews.Select(r => new AdminReviewModel
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    Date = r.Date.ToShortDateString(),
                    UserName = users.FirstOrDefault(u => u.Id == r.UserId)?.FullName ?? "Неизвестный пользователь",
                    Text = tours.FirstOrDefault(t => t.Id == r.TourId)?.Title ??
                           adminHotels.FirstOrDefault(h => h.Id == r.HotelId)?.Name ??
                           (adminFlights.FirstOrDefault(f => f.id == r.FlightId) is FlightDto fl ? $"{fl.airPlaneName} ({fl.fromCity} => {fl.toCity})" : null) ??
                           "Неизвестный объект"
                }).ToList();
                Console.WriteLine($"Отзывы обработаны: {reviews.Count}");

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в AdminDashboard.OnInitializedAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }

        private enum Tab
        {
            Tours,
            Hotels,
            Flights,
            Bookings,
            Reviews
        }
        private Tab activeTab = Tab.Tours; // State to control active tab

        private void SetActiveTab(Tab tab)
        {
            activeTab = tab;
            StateHasChanged(); // Уведомляем компонент об изменении активной вкладки
        }

        private List<HotelDto> adminHotels = new();

        private List<FlightDto> adminFlights = new();

        private List<AdminBookingModel> adminBookings = new();



        private bool showTourModal = false;
        private bool isNewTour = false;
        private TourDto editingTour = new();

        private void OpenAddTourModal()
        {
            editingTour = new TourDto();
            TourService.CreateTourAsync(editingTour);
            isNewTour = true;
            showTourModal = true;
            StateHasChanged(); // Уведомляем компонент об изменении состояния
        }

        private async Task OpenEditTourModal(TourDto tour)
        {
            var fetchedTour = await TourService.GetTourByIdAsync(tour.Id);
            if (fetchedTour != null)
            {
                editingTour = fetchedTour;
                isNewTour = false;
                showTourModal = true;
            }
        }

        private async Task SaveTour()
        {
            bool success;
            if (isNewTour)
            {
                success = await TourService.CreateTourAsync(editingTour);
            }
            else
            {
                success = await TourService.UpdateTourAsync(editingTour.Id, editingTour);
            }

            if (success)
            {
                tours = await TourService.GetToursAsync() ?? new List<TourDto>();
                showTourModal = false;
                StateHasChanged(); // Уведомляем компонент об изменении данных
            }
        }

        private void CloseTourModal()
        {
            showTourModal = false;
            StateHasChanged(); // Уведомляем компонент об изменении состояния
        }

        private bool showHotelModal = false;
        private bool isNewHotel = false;
        private HotelDto editingHotel = new();

        private void OpenAddHotelModal()
        {
            editingHotel = new HotelDto();
            isNewHotel = true;
            showHotelModal = true;
            StateHasChanged(); // Уведомляем компонент об изменении состояния
        }

        private async Task OpenEditHotelModal(HotelDto hotel)
        {
            var fetchedHotel = await HotelService.GetHotelByIdAsync(hotel.Id);
            if (fetchedHotel != null)
            {
                editingHotel = fetchedHotel;
                isNewHotel = false;
                showHotelModal = true;
            }
        }

        private async Task SaveHotel()
        {
            bool success;
            if (isNewHotel)
            {
                success = await HotelService.CreateHotelAsync(editingHotel);
            }
            else
            {
                success = await HotelService.UpdateHotelAsync(editingHotel.Id, editingHotel);
            }
            if (success)
            {
                adminHotels = await HotelService.GetHotelsAsync() ?? new List<HotelDto>();
                showHotelModal = false;
                StateHasChanged(); // Уведомляем компонент об изменении данных
            }
        }

        private void CloseHotelModal()
        {
            showHotelModal = false;
            StateHasChanged(); // Уведомляем компонент об изменении состояния
        }

        private async Task DeleteTour(int id)
        {
            var success = await TourService.DeleteTourAsync(id);
            if (success)
            {
                tours.RemoveAll(t => t.Id == id);
            }
        }

        private async Task DeleteHotel(int id)
        {
            var success = await HotelService.DeleteHotelAsync(id);
            if (success)
            {
                adminHotels.RemoveAll(h => h.Id == id);
            }
        }

        private bool showFlightModal = false;
        private bool isNewFlight = false;
        private FlightDto editingFlight = new();

        private void OpenAddFlightModal()
        {
            editingFlight = new FlightDto();
            isNewFlight = true;
            showFlightModal = true;
            StateHasChanged(); // Уведомляем компонент об изменении состояния
        }

        private async Task OpenEditFlightModal(FlightDto flight)
        {
            var fetchedFlight = await FlightService.GetFlightByIdAsync(flight.id);
            if (fetchedFlight != null)
            {
                editingFlight = fetchedFlight;
                isNewFlight = false;
                showFlightModal = true;
            }
        }

        private async Task SaveFlight()
        {
            bool success;
            if (isNewFlight)
            {
                success = await FlightService.CreateFlightAsync(editingFlight);
            }
            else
            {
                success = await FlightService.UpdateFlightAsync(editingFlight.id, editingFlight);
            }
            if (success)
            {
                adminFlights = await FlightService.GetFlightsAsync() ?? new List<FlightDto>();
                showFlightModal = false;
                StateHasChanged(); // Уведомляем компонент об изменении данных
            }
        }

        private void CloseFlightModal()
        {
            showFlightModal = false;
            StateHasChanged(); // Уведомляем компонент об изменении состояния
        }

        private async Task DeleteFlight(int id)
        {
            var success = await FlightService.DeleteFlightAsync(id);
            if (success)
            {
                adminFlights.RemoveAll(f => f.id == id);
            }
        }

        private async Task DeleteReview(int id)
        {
            var success = await ReviewService.DeleteReviewAsync(id);
            if (success)
            {
                var item = reviews.FirstOrDefault(x => x.Id == id);
                if (item != null) reviews.Remove(item);
            }
        }

        private async Task HandleTourImageUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var extension = Path.GetExtension(file.Name).ToLowerInvariant();
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".mp4")
                {
                    // Optionally add an error message here or simply ignore the file
                    return;
                }

                var rootPath = Environment.WebRootPath;
                var uploadsFolder = Path.Combine(rootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 100).CopyToAsync(stream);

                editingTour.PhotoUrl = $"/uploads/{fileName}";
            }
        }
        public async Task ConfirmBooking(int id)
        {
            var _booking = await BookingService.GetBookingByIdAsync(id);
            var users = new List<UserDto>(); // TODO: Implement GetUsersAsync in IUserService
            var user = users.FirstOrDefault(u => u.Id == _booking?.UserId);
            var userEmail = user?.Email;
            // 1. Получаем объект, который хотим изменить
            var booking = await BookingService.GetBookingByIdAsync(id);
            if (booking == null) return;

            // TODO: здесь мы должен отправить почту клиенту сообщение если админ подтверждаеть 
            booking.Status = true;

            // 2. Сохраняем изменения на сервере
            var success = await BookingService.UpdateBookingAsync(id, booking);

            if (success)
            {
                // 3. Вместо полного запроса из API, обновляем локальный список
                // Допустим, ваш список в таблице называется 'adminBookings'
                await OtpService.SendConfirmationNotificationAsync(userEmail, id);

                var localBooking = adminBookings.FirstOrDefault(b => b.Id == id);
                if (localBooking != null)
                {
                    localBooking.Status = true;
                }

                // 4. Принудительно перерисовываем UI
                StateHasChanged();
            }
        }
        public async Task DeleteBooking(int id)
        {
            var _booking = await BookingService.GetBookingByIdAsync(id);
            var users = new List<UserDto>(); // TODO: Implement GetUsersAsync in IUserService
            var user = users.FirstOrDefault(u => u.Id == _booking?.UserId);
            var userEmail = user?.Email;
            // 1. Подтверждение удаления (опционально, но рекомендуется)
            bool confirmed = await JS.InvokeAsync<bool>("Подтвердите удаление бронирования", "Вы уверены, что хотите удалить это бронирование?");
            if (!confirmed) return;

            // 2. Вызываем метод удаления на сервере
            var success = await BookingService.DeleteBookingAsync(id);

            if (success)
            {
                await OtpService.SendRejectionNotificationAsync(userEmail, id);
                // 3. Находим бронирование в локальном списке и удаляем его
                // Предполагаем, что ваш список называется 'adminBookings'
                var bookingToRemove = adminBookings.FirstOrDefault(b => b.Id == id);

                if (bookingToRemove != null)
                {
                    adminBookings.Remove(bookingToRemove);
                }

                // 4. Уведомляем Blazor, что данные изменились и нужно перерисовать UI
                StateHasChanged();
            }
        }
    }
    public class AdminBookingModel
    {
        public int Id { get; set; }
        public string? User { get; set; }
        public string? Item { get; set; }
        public string? Date { get; set; }
        public string? Price { get; set; }
        public bool Status { get; set; }
    }

    public class AdminReviewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string? Text { get; set; }
        public string? Date { get; set; }
        public string? UserName { get; set; }
        public string? Item { get; set; }
    }
}