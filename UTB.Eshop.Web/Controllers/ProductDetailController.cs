using BistroWeb.Application.Abstraction;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BistroWeb.Web.Controllers
{
    public class ProductDetailController : Controller
    {
        
        EshopDbContext _eshopDbContext;
        private readonly IFileUploadService _fileUploadService;
        public ProductDetailController(IFileUploadService fileUploadService, EshopDbContext eshopDbContext)
        {
            _fileUploadService = fileUploadService;
            _eshopDbContext = eshopDbContext;
        }

        public IActionResult Index()
        {
            var products = _eshopDbContext.Products.Include(p => p.Breweries).ToList();

            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = _eshopDbContext.Products.Include(p => p.Breweries).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
