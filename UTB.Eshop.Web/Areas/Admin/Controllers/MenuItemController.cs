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
    public class MenuItemController : Controller
    {
        IMenuItemAppService _menuitemAppService;
        EshopDbContext _eshopDbContext;
        private readonly IFileUploadService _fileUploadService;
        public Item GetItemById(int id)
        {
            return _eshopDbContext.Items.Find(id);
        }
        public MenuItemController(IMenuItemAppService menuitemAppService, EshopDbContext eshopDbContext, IFileUploadService fileUploadService)
        {
            _menuitemAppService = menuitemAppService;
            _eshopDbContext = eshopDbContext;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index(string sortOrder)
        {
            ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["SectionSortParm"] = sortOrder == "Section" ? "Section_desc" : "Section";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "Description_desc" : "Description";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";

            var items = _menuitemAppService.Select();

            switch (sortOrder)
            {
                case "ID_desc":
                    items = items.OrderByDescending(i => i.Id).ToList();
                    break;
                case "Name":
                    items = items.OrderBy(i => i.Name).ToList();
                    break;
                case "Name_desc":
                    items = items.OrderByDescending(i => i.Name).ToList();
                    break;
                case "Section":
                    items = items.OrderBy(i => i.Section).ToList();
                    break;
                case "Section_desc":
                    items = items.OrderByDescending(i => i.Section).ToList();
                    break;
                case "Price":
                    items = items.OrderBy(i => i.Price).ToList();
                    break;
                case "Price_desc":
                    items = items.OrderByDescending(i => i.Price).ToList();
                    break;
                default:
                    items = items.OrderBy(i => i.Id).ToList();
                    break;
            }

            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                await _menuitemAppService.Create(item);

                return RedirectToAction(nameof(MenuItemController.Index));
            }
            else
            {
                return View(item);
            }
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _menuitemAppService.Delete(id);

            if (deleted)
            {
                return RedirectToAction(nameof(MenuItemController.Index));
            }
            else
                return NotFound();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Item item = _menuitemAppService.GetItemById(id);

            if (item == null)
            {
                return NotFound(); // or handle appropriately
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item editedItem)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product from the database
                Item existingItem = _eshopDbContext.Items.Find(editedItem.Id);

                if (existingItem != null)
                {
                    // Update other properties
                    existingItem.Name = editedItem.Name;
                    existingItem.Description = editedItem.Description;
                    existingItem.Price = editedItem.Price;
                    existingItem.Section = editedItem.Section;
                    existingItem.Price2 = editedItem.Price2;

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
            return View(editedItem);
        }
    }
}