using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IUserServiceCrud
    {
        Task<bool> CreateUserAsync(User User);
        Task<List<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, User User);
        Task<bool> DeleteUserAsync(int id);
    }
}