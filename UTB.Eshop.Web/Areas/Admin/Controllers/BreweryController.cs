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
    public class BreweryController : Controller
    {
        IBreweryAppService _breweryAppService;
        EshopDbContext _eshopDbContext;
        private readonly IFileUploadService _fileUploadService;
        public Brewery GetBreweryById(int id)
        {
            return _eshopDbContext.Breweries.Find(id);
        }
        public BreweryController(IBreweryAppService breweryAppService, IFileUploadService fileUploadService)
        {
            _breweryAppService = breweryAppService;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            IList<Brewery> brewery = _breweryAppService.Select();
            return View(brewery);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brewery brewery)
        {
            if (ModelState.IsValid)
            {
                // Save the new brewery to the database
                await _breweryAppService.Create(brewery);

                return RedirectToAction(nameof(Index));
            }

            return View(brewery);
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _breweryAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(BreweryController.Index));
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Brewery brewery = _breweryAppService.GetBreweryById(id);

            if (brewery == null)
            {
                return NotFound(); // or handle appropriately
            }

            return View(brewery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brewery editedBrewery)
        {
            // Retrieve the existing product from the database
            Brewery existingBrewery = _breweryAppService.GetBreweryById(editedBrewery.Id);

            if (existingBrewery != null)
            {
                // Update other properties
                existingBrewery.Name = editedBrewery.Name;
                existingBrewery.Description = editedBrewery.Description;
                // Check if a new image is provided
                if (editedBrewery.Image != null)
                {
                    // Upload and update the image source
                    string newImageSrc = await _fileUploadService.FileUploadAsync(editedBrewery.Image, Path.Combine("img", "brewery"));
                    existingBrewery.ImageSrc = newImageSrc;
                }

                // Save the changes to the database
                await _breweryAppService.Edit(existingBrewery);

                return RedirectToAction(nameof(Index));
            }
            // If the model state is not valid, return to the edit view with the current model
            return View(editedBrewery);
        }

    }
}
