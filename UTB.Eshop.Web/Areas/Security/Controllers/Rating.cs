using Microsoft.AspNetCore.Mvc;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BistroWeb.Application.Abstraction;

// Ensure your project is using these namespaces

namespace BistroWeb.Web.Controllers
{
    [Authorize] // This ensures that only authenticated users can access methods in this controller
    public class RatingsController : Controller
    {
        private readonly IRatingTableAppService _ratingService;
        private readonly UserManager<IdentityUser> _userManager; // Adjust IdentityUser to your user entity if it's different

        public RatingsController(IRatingTableAppService ratingService, UserManager<IdentityUser> userManager)
        {
            _ratingService = ratingService;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyRatings()
        {
            var userId = _userManager.GetUserId(User); // This gets the current logged-in user ID as a string
            var ratings = await _ratingService.GetRatingsByUserIdAsync(userId);

            var model = new UserProfileViewModel
            {
                // Populate the model with the fetched ratings
                Rated = ratings
            };

            return View(model);
        }

    }
}
