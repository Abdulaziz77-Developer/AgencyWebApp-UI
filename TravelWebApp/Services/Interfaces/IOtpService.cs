namespace TravelWebApp.Services.Interfaces
{
    public interface IOtpService
    {
        Task<bool> SendOtpAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string code);
    }
}
