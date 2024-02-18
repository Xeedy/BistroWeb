using Microsoft.AspNetCore.Mvc;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Application.Implementation;

namespace BistroWeb.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class MissingController : Controller
    {
        IMissingAppService _missingAppService;
        EshopDbContext _eshopDbContext;
        public Missing GetMissingById(int id)
        {
            return _eshopDbContext.Missings.Find(id);
        }
        public MissingController(IMissingAppService missingAppService, EshopDbContext eshopDbContext)
        {
            _missingAppService = missingAppService;
            _eshopDbContext = eshopDbContext;
        }

        public IActionResult Index(string sortOrder)
        {
            var missing = _missingAppService.Select();

            return View(missing);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Missing missing)
        {
            if (ModelState.IsValid)
            {
                await _missingAppService.Create(missing);

                return RedirectToAction(nameof(MissingController.Index));
            }
            else
            {
                return View(missing);
            }
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _missingAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(MissingController.Index));
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Missing missing = _missingAppService.GetMissingById(id);

            if (missing == null)
            {
                return NotFound(); // or handle appropriately
            }

            return View(missing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Missing editedMissing)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product from the database
                Missing existingMissing = _eshopDbContext.Missings.Find(editedMissing.Id);

                if (existingMissing != null)
                {
                    // Update other properties
                    existingMissing.Name = editedMissing.Name;
                    existingMissing.Description = editedMissing.Description;

                    // Save changes to the database
                    await _eshopDbContext.SaveChangesAsync(); // Use SaveChangesAsync if your _eshopDbContext supports it

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound(); // or handle appropriately
                }
            }

            // If the model state is not valid, return to the edit view with the current model
            return View(editedMissing);
        }
    }
}
