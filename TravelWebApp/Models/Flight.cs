using System;

namespace TravelWebApp.Models
{
    public class Flight
    {
        public int id { get; set; }
        public string airPlaneName { get; set; } = "";
        public int flightNumber { get; set; }   
        public string? fromCity { get; set; }
        public string? toCity { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
        public decimal Price { get; set; } = new Random().Next(100, 1000);
    }
}