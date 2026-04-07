namespace AgencyWebApp.Client.Infrastructure.Api.Dtos.Auth;

public class AuthResultDto
{
    public int UserId { get; set; }
    
    public string Role { get; set; } = string.Empty;
}