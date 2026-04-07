using System;

namespace AgencyWebApp.Client.Infrastructure.Api.Dtos.Bookings;

public class BookingDto
{
    public int Id { get; set; }
    
    public DateTime BookingDate { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    // Статус: оплачено/не оплачено или подтверждено/нет
    public bool Status { get; set; } 
    
    public string CustomerName { get; set; } = string.Empty;
    
    public string CustomerEmail { get; set; } = string.Empty;
    
    public int UserId { get; set; }
    
    // Внешние ключи (nullable, так как бронь может быть только на тур или только на отель)
    public int? HotelId { get; set; }
    public int? TourId { get; set; }
    public int? FlightId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}