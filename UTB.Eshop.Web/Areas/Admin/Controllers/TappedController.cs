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
    public class TappedController : Controller
    { 
        ITappedAppService _tappedAppService;
        EshopDbContext _eshopDbContext;
        public TappedController(EshopDbContext eshopDbContext, ITappedAppService tappedAppService)
        {
            _eshopDbContext = eshopDbContext;
            _tappedAppService = tappedAppService;
        }

        public IActionResult Index()
        {
            var tappeds = _eshopDbContext.Tappeds.Include(p => p.Breweries).Include(p => p.Typees).ToList();

            return View(tappeds);
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
        public async Task<IActionResult> Create(Tapped model)
        {
            _eshopDbContext.Tappeds.Add(model);
            await _eshopDbContext.SaveChangesAsync(); // Use async SaveChangesAsync
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            bool deleted = _tappedAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(TappedController.Index));
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
            var tapped = _eshopDbContext.Tappeds.Include(p => p.Breweries).Include(p => p.Typees).FirstOrDefault(p => p.Id == id);

            if (tapped == null)
            {
                NotFound();
            }

            return View(tapped);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Tapped model)
        {
            ModelState.Remove("Breweries");
            ModelState.Remove("Typees");
            if (ModelState.IsValid)
            {
                
                _eshopDbContext.Tappeds.Update(model);
                await _eshopDbContext.SaveChangesAsync(); // Use async SaveChangesAsync
                return RedirectToAction(nameof(Index));
            }
            LoadBreweries();
            LoadTypees();
            return View(model);
        }
    }
}