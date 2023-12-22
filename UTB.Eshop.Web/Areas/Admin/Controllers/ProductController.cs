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
            var product = _productAppService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            var breweries = _breweryAppService.GetAllBreweries().ToList();

            var viewModel = new BreweryProductViewModel
            {
                Products = new List<Product> { product },
                Breweries = breweries
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BreweryProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _productAppService.GetProductById(viewModel.Products[0].Id);

                if (existingProduct != null)
                {
                    // Update other properties
                    existingProduct.Name = viewModel.Products[0].Name;
                    existingProduct.Brewery = viewModel.Products[0].Brewery;
                    existingProduct.Description = viewModel.Products[0].Description;
                    existingProduct.Price = viewModel.Products[0].Price;

                    if (viewModel.Products[0].Image != null)
                    {
                        string newImageSrc = await _fileUploadService.FileUploadAsync(viewModel.Products[0].Image, Path.Combine("img", "product"));
                        existingProduct.ImageSrc = newImageSrc;
                    }

                    await _productAppService.Edit(existingProduct);

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(viewModel);
        }




    }
}