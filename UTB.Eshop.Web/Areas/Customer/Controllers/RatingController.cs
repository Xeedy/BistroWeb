using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BistroWeb.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class RatingController : Controller
    {
        private readonly IRatingTableAppService _ratingService;

        public RatingController(IRatingTableAppService ratingService)
        {
            _ratingService = ratingService;
        }

        public class RateProductRequest
        {
            public int ProductId { get; set; }
            public int RatingValue { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> RateProduct([FromBody] RateProductRequest request)
        {
            // Validate the request (ensure the rating is within the allowed range and the product exists)

            // Get the logged-in user's ID. This assumes you're using ASP.NET Core Identity.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user id from the claims

            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }

            // Create and save the rating
            var rating = new Rating
            {
                ProductId = request.ProductId,
                UserId = int.Parse(userId), // Assuming your UserId is an int
                RatingValue = request.RatingValue
            };

            await _ratingService.Create(rating);

            return Ok(new { message = "Rating submitted successfully" });
        }

        [HttpGet("{id}")]
        public IActionResult GetRatingById(int id)
        {
            var rating = _ratingService.GetRatingById(id);
            if (rating != null)
            {
                return Ok(rating);
            }
            return NotFound("Rating not found.");
        }
    }
}
