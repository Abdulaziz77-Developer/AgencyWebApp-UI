using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDTO>> GetReviewsAsync();
        Task<bool> DeleteReviewAsync(int id);
    }
}