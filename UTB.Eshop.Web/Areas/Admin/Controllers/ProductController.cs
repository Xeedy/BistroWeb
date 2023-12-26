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
        public ProductController(IFileUploadService fileUploadService, EshopDbContext eshopDbContext)
        {
            _fileUploadService = fileUploadService;
            _eshopDbContext = eshopDbContext;
        }

        public IActionResult Index()
        {
            var products = _eshopDbContext.Products.Include("Breweries");

            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            LoadBreweries();
            return View();
        }
        [NonAction]
        private void LoadBreweries()
        {
            var breweries = _eshopDbContext.Breweries.ToList();
            ViewBag.Breweries = new SelectList(breweries, "Id", "Name");
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
            _eshopDbContext.Products.Add(model);
            _eshopDbContext.SaveChanges();
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

            // Eager load Breweries
            var product = _eshopDbContext.Products.Include(p => p.Breweries).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            ModelState.Remove("Breweries");
            if (!ModelState.IsValid)
            {
                _eshopDbContext.Products.Update(model);
                _eshopDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


    }
}