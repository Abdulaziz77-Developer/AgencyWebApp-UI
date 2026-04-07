using System.Collections.Generic;
using System.Threading.Tasks;
using TravelWebApp.Infrastrcuture.api.dtos.Reviews;
using TravelWebApp.Models;

namespace TravelWebApp.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetReviewsAsync();
        Task<bool> DeleteReviewAsync(int id);
    }
}