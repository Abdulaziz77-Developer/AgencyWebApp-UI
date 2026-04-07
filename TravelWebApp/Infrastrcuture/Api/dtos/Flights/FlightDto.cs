using System;

namespace AgencyWebApp.Client.Infrastructure.Api.Dtos.Flights;

public class FlightDto
{
  
    public int id { get; set; }
    
    public string airPlaneName { get; set; } = string.Empty;
    
    public int flightNumber { get; set; }   
    
    public string? fromCity { get; set; }
    
    public string? toCity { get; set; }
    
    public DateTime departureTime { get; set; }
    
    public DateTime arrivalTime { get; set; }
    
    public decimal Price { get; set; }
}