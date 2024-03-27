using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Web.Models;
using BistroWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BistroWeb.Application.Implementation;
using BistroWeb.Infrastructure.Database;

namespace BistroWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IHomeService _homeService;
        IBreweryAppService _breweryAppService;
        private readonly IRatingTableAppService _ratingTableAppService;
        private readonly IProductAppService _productAppService;
        private readonly EshopDbContext _eshopDbContext;
        public HomeController(ILogger<HomeController> logger,
                                IHomeService homeService,
                                IProductAppService productAppService,
                                IRatingTableAppService ratingTableAppService,
                                EshopDbContext eshopDbContext)
        {
            _logger = logger;
            _eshopDbContext = eshopDbContext;
            _homeService = homeService;
            _ratingTableAppService = ratingTableAppService;
        }

        public IActionResult Index()
        {
            CarouselProductViewModel viewModel = _homeService.GetIndexViewModel();
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Lahve()
        {
            IEnumerable<Product> products = _homeService.GetProducts();
            return View(products);
        }

        public IActionResult HookahMenu()
        {
            return View();
        }
        public IActionResult Menu()
        {
            MenuItemViewModel viewModel = _homeService.GetIndexViewModel2();
            return View(viewModel);
        }
        public IActionResult Accesories()
        {
            return View();
        }

        public async Task<IActionResult> AllProductRatings()
        {
            var products = await _eshopDbContext.Products
                .Include(p => p.Breweries)
                .Include(p => p.Typees)
                .ToListAsync();

            var productRatingsViewModels = new List<ProductDetailViewModel>();

            foreach (var product in products)
            {
                double averageRating = await _ratingTableAppService.GetAverageRatingForProductAsync(product.Id);

                productRatingsViewModels.Add(new ProductDetailViewModel
                {
                    Product = product,
                    AverageRating = averageRating,
                    UserRating = null // Since this is a general list, individual user ratings might not be applicable here
                });
            }

            return View(productRatingsViewModels);
        }
    }
}

