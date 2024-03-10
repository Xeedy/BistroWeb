using BistroWeb.Application.Abstraction;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            var viewModel = new CalendarViewModel
            {
                Calendars = await _calendarAppService.GetAllCalendarsAsync(),
                Shifts = await _calendarAppService.GetAllShiftsAsync()
            };

            return View(viewModel);
        }
    }
}
