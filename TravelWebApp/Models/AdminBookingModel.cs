namespace TravelWebApp.Models
{
    public class AdminBookingModel {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User { get; set; } = "";
        public string Item { get; set; } = "";
        public string Date { get; set; } = "";
        public string Price { get; set; } = "";
    }
}