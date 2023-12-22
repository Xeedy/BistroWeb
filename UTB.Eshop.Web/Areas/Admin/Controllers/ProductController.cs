using Microsoft.AspNetCore.Mvc;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            // Pass the breweries to the view
            ViewBag.Breweries = _breweryAppService.GetBreweries();

            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CarouselProductViewModel
            {
                Products = new List<Product> { new Product() }, // Initialize with a new product
                Brewery = _breweryAppService.GetBreweries().ToList(),
                SelectedBreweryId = 0 // Set the default selected value as needed
            };

            // Use ViewData to pass the Breweries to the view
            ViewData["Breweries"] = new SelectList(viewModel.Brewery, "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarouselProductViewModel viewModel)
        {
            // Check if the list of products is not null and contains at least one element
            if (viewModel.Products != null && viewModel.Products.Any())
            {
                // Assuming you want to create a new product
                Product newProduct = viewModel.Products.FirstOrDefault();

                if (newProduct != null)
                {
                    // Set the BreweryId for the new product
                    newProduct.BreweryId = viewModel.SelectedBreweryId;

                    // Your existing code to upload and create the product
                    await _productAppService.Create(newProduct);

                    return RedirectToAction(nameof(Index));
                }
            }

            // If the list is null or empty, handle it accordingly (maybe return to the same view with an error message)
            ModelState.AddModelError(string.Empty, "No product data provided.");
            viewModel.Brewery = _breweryAppService.GetBreweries().ToList();

            // Use ViewData to pass the Breweries to the view
            ViewData["Breweries"] = new SelectList(viewModel.Brewery, "Id", "Name");

            return View(viewModel);
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

            // Assuming CarouselProductViewModel has a property named Breweries to populate the dropdown
            CarouselProductViewModel viewModel = new CarouselProductViewModel
            {
                Products = new List<Product> { existingProduct },
                Brewery = _breweryAppService.GetBreweries()?.ToList() ?? new List<Brewery>(),
                SelectedBreweryId = existingProduct.BreweryId.HasValue ? (int)existingProduct.BreweryId : 0 // Set the default selected value based on existing product
            };
            ViewData["Breweries"] = new SelectList(viewModel.Brewery, "Id", "Name", viewModel.SelectedBreweryId);
            Console.WriteLine($"Product Id: {existingProduct.Id}");
            Console.WriteLine($"Product Name: {existingProduct.Name}");
            Console.WriteLine($"Product Description: {existingProduct.Description}");
            Console.WriteLine($"Product Price: {existingProduct.Price}");
            Console.WriteLine($"BreweryId: {existingProduct.BreweryId}");


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarouselProductViewModel viewModel)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                // If the model is not valid, return to the view with validation errors
                viewModel.Brewery = _breweryAppService.GetBreweries()?.ToList() ?? new List<Brewery>();
                return View(viewModel);
            }

            // Get the existing product for the given id
            Product existingProduct = _productAppService.GetProductById(viewModel.Products[0].Id);

            if (existingProduct == null)
            {
                return NotFound(); // or handle appropriately
            }

            // Update other properties
            existingProduct.Name = viewModel.Products[0].Name;
            existingProduct.Description = viewModel.Products[0].Description;
            existingProduct.Price = viewModel.Products[0].Price;
            existingProduct.BreweryId = viewModel.SelectedBreweryId;

            // If a new image is provided, upload and update the image source
            if (viewModel.Products[0].Image != null)
            {
                string newImageSrc = await _fileUploadService.FileUploadAsync(viewModel.Products[0].Image, Path.Combine("img", "products"));
                existingProduct.ImageSrc = newImageSrc;
            }

            try
            {
                // Save changes to the database
                await _productAppService.Edit(existingProduct);

                // Redirect to the Index action
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                foreach (var modelStateEntry in ModelState)
                {
                    var key = modelStateEntry.Key;
                    var errors = modelStateEntry.Value.Errors;

                    foreach (var error in errors)
                    {
                        Console.WriteLine($"ModelState Error for {key}: {error.ErrorMessage}");
                    }
                }

                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "Error updating product.");
                viewModel.Brewery = _breweryAppService.GetBreweries()?.ToList() ?? new List<Brewery>();
                return View(viewModel);
            }
        }




    }
}