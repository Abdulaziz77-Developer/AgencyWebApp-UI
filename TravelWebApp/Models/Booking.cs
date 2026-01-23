using System;

namespace TravelWebApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; } 
        public string CustomerName { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public int UserId { get; set; }
        public int? HotelId { get; set; }
        public int? TourId { get; set; }
        public int? FlightId { get; set; }
        public DateTime CreatedAt { get; internal set; }
    }
}