using System.Threading.Tasks;

namespace TravelWebApp.Infrastrcuture.api.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string confirmPassword);

    }
}