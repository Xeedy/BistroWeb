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
        Rating GetRatingById(int id); // Add this method to the interface
    }
}
