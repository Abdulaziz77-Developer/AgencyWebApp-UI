namespace TravelWebApp.Infrastrcuture.api.Services.Interfaces
{
    public interface IOtpService
    {
        Task<bool> SendOtpAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string code);
        Task<bool> SendConfirmationNotificationAsync(string email, int bookingId);
        Task<bool> SendRejectionNotificationAsync(string email, int bookingId);
    }
}
