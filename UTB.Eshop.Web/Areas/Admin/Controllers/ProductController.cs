using Microsoft.AspNetCore.Mvc;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;

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
            List<Brewery> breweries = _breweryAppService.GetAllBreweries().ToList();

            var viewModel = new ProductViewModel
            {
                Product = new Product(),
                Breweries = breweries
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productAppService.Create(product);

                return RedirectToAction(nameof(ProductController.Index));
            }
            else
            {
                return View(product);
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
            var product = _productAppService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var breweries = _breweryAppService.GetAllBreweries().ToList();

            var viewModel = new ProductViewModel
            {
                Product = product,
                Breweries = breweries
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product from the database
                var existingProduct = _productAppService.GetProductById(viewModel.Product.Id);

                if (existingProduct != null)
                {
                    // Update other properties
                    existingProduct.Name = viewModel.Product.Name;
                    existingProduct.Brewery = viewModel.Product.Brewery;
                    existingProduct.Description = viewModel.Product.Description;
                    existingProduct.Price = viewModel.Product.Price;

                    // Check if a new image is provided
                    if (viewModel.Product.Image != null)
                    {
                        // Upload and update the image source
                        string newImageSrc = await _fileUploadService.FileUploadAsync(viewModel.Product.Image, Path.Combine("img", "product"));
                        existingProduct.ImageSrc = newImageSrc;
                    }

                    // Save the changes to the database
                    await _productAppService.Edit(existingProduct);

                    return RedirectToAction(nameof(Index));
                }
            }
            // If the model state is not valid, return to the edit view with the current model
            return View(viewModel);
        }




    }
}