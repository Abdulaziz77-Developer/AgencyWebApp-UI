using System.Threading.Tasks;

namespace TravelWebApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string confirmPassword);

    }
}