using BistroWeb.Application.Abstraction;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace BistroWeb.Web.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRatingTableAppService _ratingService;
        // Assuming you might need access to the database context directly
        private readonly EshopDbContext _eshopDbContext;

        public ProductDetailController(UserManager<User> userManager, IRatingTableAppService ratingService, EshopDbContext eshopDbContext)
        {
            _userManager = userManager;
            _ratingService = ratingService;
            _eshopDbContext = eshopDbContext; // Only add this if you need direct access to the DbContext
        }



        public IActionResult Index()
        {
            var products = _eshopDbContext.Products.Include(p => p.Breweries).Include(p => p.Typees).ToList();

            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = await _eshopDbContext.Products
                .Include(p => p.Breweries)
                .Include(p => p.Typees)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            int? userRating = null;
            if (!string.IsNullOrEmpty(userId))
            {
                // Corrected variable name here
                userRating = await _ratingService.GetUserRatingForProduct(id, userId);
            }
            double averageRating = await _ratingService.GetAverageRatingForProductAsync(id);
            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                UserRating = userRating,
                AverageRating = averageRating
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RateProduct(int productId, int ratingValue)
        {
            // This gets the ID of the currently logged-in user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Unauthorized("User must be logged in to rate products.");
            }

            // Use user.Id directly since it's already an int
            await _ratingService.CreateOrUpdateRating(new Rating
            {
                ProductId = productId,
                UserId = user.Id, // No conversion necessary
                RatingValue = ratingValue
            });

            TempData["SuccessMessage"] = "Rating submitted successfully!";
            return RedirectToAction("Details", new { id = productId });
        }

    }
}

