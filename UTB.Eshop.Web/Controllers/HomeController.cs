using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Web.Models;

namespace BistroWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IHomeService _homeService;
        IBreweryAppService _breweryAppService;

        public HomeController(ILogger<HomeController> logger,
                                IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult FilterProductsByBrewery(int breweryId)
        {
            // Implement your logic to filter products based on the selected brewery
            var filteredProducts = _breweryAppService.GetProductsByBrewery(breweryId);

            // Return a partial view with the filtered products
            return PartialView("_FilteredProductsPartialView", filteredProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Lahve()
        {
            CarouselProductViewModel viewModel = _homeService.GetIndexViewModel();
            return View(viewModel);
        }
        public IActionResult HookahMenu()
        {
            return View();
        }
        public IActionResult Menu()
        {
            CarouselProductViewModel viewModel = _homeService.GetIndexViewModel();
            return View(viewModel);
        }
        public IActionResult Accesories()
        {
            return View();
        }
    }
}

