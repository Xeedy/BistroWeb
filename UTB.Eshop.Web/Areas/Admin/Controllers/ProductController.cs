using Microsoft.AspNetCore.Mvc;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BistroWeb.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductController : Controller
    {
        IProductAppService _productAppService;
        EshopDbContext _eshopDbContext;
        IBreweryAppService _breweryAppService;
        private readonly IFileUploadService _fileUploadService;
        public Product GetProductById(int id)
        {
            return _eshopDbContext.Products.Find(id);
        }
        public ProductController(IProductAppService productAppService, IFileUploadService fileUploadService, IBreweryAppService breweryAppService, EshopDbContext eshopDbContext)
        {
            _productAppService = productAppService;
            _fileUploadService = fileUploadService;
            _breweryAppService = breweryAppService;
            _eshopDbContext = eshopDbContext;
        }

        public IActionResult Index()
        {
            IList<Product> products = _productAppService.Select();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CarouselProductViewModel
            {
                Brewery = _breweryAppService.GetBreweries().ToList()
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CarouselProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Assuming you want to create a new product
                var newProduct = viewModel.Products[0];

                // Set the BreweryId for the new product
                newProduct.BreweryId = viewModel.SelectedBreweryId;

                await _productAppService.Create(newProduct);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Populate the breweries again in case of model validation failure
                ViewBag.Breweries = _breweryAppService.GetBreweries(); // Assuming you have a method to get breweries
                return View(viewModel);
            }
        }


        public IActionResult Delete(int id)
        {
            bool deleted = _productAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(ProductController.Index));
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Get the existing product for the given id
            Product existingProduct = _productAppService.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound(); // or handle appropriately
            }

            // Get the associated brewery for the existing product
            Brewery existingBrewery = existingProduct.BreweryId.HasValue
                ? _breweryAppService.GetBreweryById(existingProduct.BreweryId.Value)
                : null;

            // Construct the view model
            var viewModel = new CarouselProductViewModel
            {
                Products = new List<Product> { existingProduct },
                Brewery = _breweryAppService.GetBreweries()?.ToList() ?? new List<Brewery>(),
                SelectedBreweryId = existingProduct.BreweryId ?? 0 // Handle the nullable case
            };

            return View(viewModel);
        }

    }
}