using BistroWeb.Application.Abstraction;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using BistroWeb.Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BistroWeb.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CalendarController : Controller
    {
        private readonly ICalendarAppService _calendarAppService;
        public CalendarController(ICalendarAppService calendarAppService)
        {
            _calendarAppService = calendarAppService;
        }

        public async Task<IActionResult> Index(int? year, int? month)
        {
            var now = DateTime.Now;

            // Call GetCalendarViewModelAsync to initialize your viewModel
            var viewModel = await _calendarAppService.GetCalendarViewModelAsync(year ?? now.Year, month ?? now.Month);

            // Now, fetch the list of Managers using _calendarAppService and assign it to viewModel.Managers
            viewModel.Managers = await _calendarAppService.GetManagersAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignShift(DateTime date, int userId)
        {
            await _calendarAppService.AssignOrUpdateShiftAsync(date, userId);
            return RedirectToAction(nameof(Index));
        }

    }
}
