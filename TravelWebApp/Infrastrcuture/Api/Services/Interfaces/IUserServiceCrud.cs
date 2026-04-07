using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Infrastrcuture.api.dtos.Users;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IUserServiceCrud
    {
        Task<bool> CreateUserAsync(UserDto User);
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UserDto User);
        Task<bool> DeleteUserAsync(int id);
    }
}