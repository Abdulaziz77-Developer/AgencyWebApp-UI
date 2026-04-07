namespace AgencyWebApp.Client.Infrastructure.Api.Dtos.Bookings;

public class BookingCreatedDto
{
    public int UserId { get; set; }
    
    // Используем int?, так как пользователь может бронировать что-то одно
    public int? HotelId { get; set; }
    
    public int? TourId { get; set; }
    
    public int? FlightId { get; set; }
}