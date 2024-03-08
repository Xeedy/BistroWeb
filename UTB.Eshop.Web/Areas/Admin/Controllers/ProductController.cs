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
        private readonly IFileUploadService _fileUploadService;
        public ProductController(IFileUploadService fileUploadService, EshopDbContext eshopDbContext, IProductAppService productAppService)
        {
            _fileUploadService = fileUploadService;
            _eshopDbContext = eshopDbContext;
            _productAppService = productAppService;
        }

        public IActionResult Index()
        {
            var products = _eshopDbContext.Products.Include(p => p.Breweries).Include(p => p.Typees).ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadBreweries();
            LoadTypees();
            return View();
        }
        [NonAction]
        private void LoadBreweries()
        {
            var breweries = _eshopDbContext.Breweries.ToList();
            ViewBag.Breweries = new SelectList(breweries, "Id", "Name");
        }
        private void LoadTypees()
        {
            var typees = _eshopDbContext.Typees.ToList();
            ViewBag.Typees = new SelectList(typees, "Id", "Name");
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            string imageSrc = await _fileUploadService.FileUploadAsync(model.Image, Path.Combine("img", "model"));
            model.ImageSrc = imageSrc;
            _eshopDbContext.Products.Add(model);
            await _eshopDbContext.SaveChangesAsync(); // Use async SaveChangesAsync
            return RedirectToAction(nameof(Index));
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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                NotFound();
            }

            LoadBreweries();
            LoadTypees();

            // Eager load Breweries
            var product = _eshopDbContext.Products.Include(p => p.Breweries).Include(p => p.Typees).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            ModelState.Remove("Breweries");
            ModelState.Remove("Typees");
            if (ModelState.IsValid)
            {
                // Check if a new image is provided
                if (model.Image != null)
                {
                    // Upload and update the image source
                    string newImageSrc = await _fileUploadService.FileUploadAsync(model.Image, Path.Combine("img", "model"));
                    model.ImageSrc = newImageSrc;
                }

                _eshopDbContext.Products.Update(model);
                await _eshopDbContext.SaveChangesAsync(); // Use async SaveChangesAsync
                return RedirectToAction(nameof(Index));
            }

            LoadBreweries(); // Ensure that breweries are loaded for the view
            LoadTypees(); // Ensure that breweries are loaded for the view
            return View(model);
        }



    }
}