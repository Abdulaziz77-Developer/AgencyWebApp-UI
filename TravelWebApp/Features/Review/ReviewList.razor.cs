using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using TravelWebApp.Infrastrcuture.api.Services.Interfaces;

namespace TravelWebApp.Features.Review
{
    public partial class ReviewList : ComponentBase
    {
        [Inject] public IReviewService ReviewService { get; set; }
        [Inject] public IUserServiceCrud UserService { get; set; }
        private List<AdminReviewModel>? reviews;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var rawData = await ReviewService.GetReviewsAsync();

                if (rawData != null)
                {
                    reviews = rawData.Select(r => new AdminReviewModel
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        Text = r.Text,
                        TourId = r.TourId,
                        HotelId = r.HotelId,
                        FlightId = r.FlightId,
                        Date = r.Date
                    }).ToList();

                    // Загружаем имена пользователей параллельно для скорости
                    var tasks = reviews.Select(async review =>
                    {
                        if (string.IsNullOrWhiteSpace(review.UserName))
                        {
                            var user = await UserService.GetUserByIdAsync(review.UserId);
                            review.UserName = user?.FullName ?? $"Клиент #{review.UserId}";
                        }
                    });

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в компоненте отзывов: {ex.Message}");
                reviews = new List<AdminReviewModel>();
            }
        }

        public class AdminReviewModel
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int? TourId { get; set; }
            public int? FlightId { get; set; }
            public int? HotelId { get; set; }
            public string? Text { get; set; }

            // Ключевой момент: проверь как называется поле в JSON API
            [JsonPropertyName("createdAt")]
            public DateTime? Date { get; set; }

            public string? UserName { get; set; }
        }
    }
}