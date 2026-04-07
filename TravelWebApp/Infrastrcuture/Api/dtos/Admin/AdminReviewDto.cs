namespace AgencyWebApp.Client.Infrastructure.Api.Dtos.Admin;

public class AdminReviewDto
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public string UserName { get; set; } = string.Empty;
    
    public string Text { get; set; } = string.Empty;
    
    public int ItemId { get; set; }
    
    public string Item { get; set; } = string.Empty;
    
    public string Date { get; set; } = string.Empty;
    
    public string Price { get; set; } = string.Empty;
}