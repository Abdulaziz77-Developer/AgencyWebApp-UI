namespace TravelWebApp.Models
{
    public class BookingCreated
    {
        public int UserId { get;set; }
        public int? HotelId { get; set; }
        public int? TourId { get; set; }
        public int? FlightId { get; set; }
    }
}
