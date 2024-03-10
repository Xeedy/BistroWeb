using System.Collections.Generic;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface IRatingTableAppService
    {
        IList<Rating> Select();
        Task Create(Rating rating);
        bool Delete(int id);
        Task Edit(Rating editedRating);
        Rating GetRatingById(int id); // Method signature already there
        Task<int?> GetUserRatingForProduct(int productId, string userId); // Add this signature
        Task CreateOrUpdateRating(Rating rating);
        Task<double> GetAverageRatingForProductAsync(int productId);
        Task<List<Rating>> GetRatingsAsync();
        string GetProductNameById(int productId);
    }
}
