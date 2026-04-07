using System.Text.Json.Serialization;

namespace TravelWebApp.Infrastrcuture.api.dtos.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [JsonPropertyName("fullName")]
        public string UserName { get; set; } = "";
        public string Text { get; set; } = "";
        public int Rating { get; set; }

        // Маппинг для API
        [JsonPropertyName("createdAt")]
        public DateTime Date { get; set; }

        // Используем nullable int?, чтобы корректно принять null из API
        public int? TourId { get; set; }
        public int? HotelId { get; set; }
        public int? FlightId { get; set; }

        // Дополнительное поле для названия (Тур "Альпы" и т.д.)
        public string ItemDisplayName { get; set; } = "";
    }
}
