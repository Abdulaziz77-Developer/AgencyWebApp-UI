using System;

namespace TravelWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId{ get; set; }
        public string UserName { get; set; } = "";
        public string Item { get; set; } = "";
        public int ItemId { get; set; }
        public string Text { get; set; } = "";
        public int TourId { get; set; }
        public int FlightId { get; set; }
        public int HotelId{ get; set; } 
        public int Rating { get; set; }
        public DateTime Date { get; set; } 
    }
}