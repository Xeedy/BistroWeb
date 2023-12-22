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
        public IActionResult Create(Product product)
        {
            var viewModel = new BreweryProductViewModel
            {
                Products = new List<Product> { new Product() }, // Initialize the list with at least one product
                Breweries = _breweryAppService.GetAllBreweries()?.ToList() ?? new List<Brewery>()
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(BreweryProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Assuming you want to create a new product
                var newProduct = viewModel.Products[0];
                await _productAppService.Create(newProduct);

                return RedirectToAction(nameof(Index));
            }
            else
            {
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
            var viewModel = new BreweryProductViewModel
            {
                Products = new List<Product> { existingProduct },
                Breweries = _breweryAppService.GetAllBreweries()?.ToList() ?? new List<Brewery>(),
                SelectedBreweryId = existingProduct.BreweryId ?? 0 // Handle the nullable case
            };

            return View(viewModel);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BreweryProductViewModel editedViewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product from the database
                Product existingProduct = _productAppService.GetProductById(editedViewModel.Products[0].Id);

                if (existingProduct != null)
                {
                    // Update other properties
                    existingProduct.Name = editedViewModel.Products[0].Name;
                    existingProduct.Description = editedViewModel.Products[0].Description;
                    existingProduct.Price = editedViewModel.Products[0].Price;

                    // Check if a new image is provided
                    if (editedViewModel.Products[0].Image != null)
                    {
                        // Upload and update the image source
                        string newImageSrc = await _fileUploadService.FileUploadAsync(editedViewModel.Products[0].Image, Path.Combine("img", "products"));
                        existingProduct.ImageSrc = newImageSrc;
                    }

                    // Update breweryId
                    existingProduct.BreweryId = editedViewModel.Products[0].BreweryId;

                    // Save the changes to the database
                    await _productAppService.Edit(existingProduct);

                    return RedirectToAction(nameof(Index));
                }
            }

            // If the model state is not valid or existingProduct is not found, return to the edit view with the current model
            return View(editedViewModel);
        }


    }
}