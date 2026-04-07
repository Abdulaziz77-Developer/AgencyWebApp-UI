namespace AgencyWebApp.Client.Infrastructure.Api.Dtos.Admin;

public class AdminBookingDto 
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    // Оставляем string, как в твоем старом проекте
    public string User { get; set; } = string.Empty;
    
    public string Item { get; set; } = string.Empty;
    
    public string Date { get; set; } = string.Empty;
    
    public string Price { get; set; } = string.Empty;
}