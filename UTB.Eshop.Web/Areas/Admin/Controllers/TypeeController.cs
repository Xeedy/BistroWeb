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
    public class TypeeController : Controller
    {
        ITypeeAppService _typeeAppService;
        EshopDbContext _eshopDbContext;
        public Typee GetTypeeById(int id)
        {
            return _eshopDbContext.Typees.Find(id);
        }
        public TypeeController(ITypeeAppService typeeAppService, EshopDbContext eshopDbContext)
        {
            _typeeAppService = typeeAppService;
            _eshopDbContext = eshopDbContext;
        }

        public IActionResult Index()
        {
            var typee = _typeeAppService.Select();
            return View(typee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Typee typee)
        {
            if (ModelState.IsValid)
            {
                await _typeeAppService.Create(typee);

                return RedirectToAction(nameof(TypeeController.Index));
            }
            else
            {
                return View(typee);
            }
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _typeeAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(TypeeController.Index));
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Typee typee = _typeeAppService.GetTypeeById(id);

            if (typee == null)
            {
                return NotFound(); // or handle appropriately
            }

            return View(typee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Typee editedTypee)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product from the database
                Typee existingTypee = _eshopDbContext.Typees.Find(editedTypee.Id);

                if (existingTypee != null)
                {
                    // Update other properties
                    existingTypee.Name = editedTypee.Name;
                    existingTypee.Description = editedTypee.Description;

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
            return View(editedTypee);
        }
    }
}
