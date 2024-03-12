using BistroWeb.Application.Abstraction;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using BistroWeb.Infrastructure.Identity;
using BistroWeb.Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BistroWeb.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CalendarController : Controller
    {
        private readonly ICalendarAppService _calendarAppService;
        private readonly UserManager<User> _userManager;

        public CalendarController(ICalendarAppService calendarAppService, UserManager<User> userManager)
        {
            _calendarAppService = calendarAppService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? year, int? month)
        {
            var now = DateTime.Now;
            // Ensure year and month have valid values
            int currentYear = year ?? now.Year;
            int currentMonth = month ?? now.Month;

            // Adjust for out-of-range month values and correct the year accordingly
            if (currentMonth < 1)
            {
                currentYear--;
                currentMonth = 12;
            }
            else if (currentMonth > 12)
            {
                currentYear++;
                currentMonth = 1;
            }

            // Obtain the current user
            var currentUser = await _userManager.GetUserAsync(User);

            // Generate the calendar view model for the given year and month
            var viewModel = await _calendarAppService.GetCalendarViewModelAsync(currentYear, currentMonth);

            // Calculate navigation parameters for the previous and next months
            var firstDayOfCurrentMonth = new DateTime(currentYear, currentMonth, 1);
            var firstDayOfNextMonth = firstDayOfCurrentMonth.AddMonths(1);
            var firstDayOfPreviousMonth = firstDayOfCurrentMonth.AddMonths(-1);

            // Set navigation properties in the ViewModel
            viewModel.PreviousMonthYear = firstDayOfPreviousMonth.Year;
            viewModel.PreviousMonth = firstDayOfPreviousMonth.Month;
            viewModel.NextMonthYear = firstDayOfNextMonth.Year;
            viewModel.NextMonth = firstDayOfNextMonth.Month;

            // Set the ID of the current user into the ViewModel
            viewModel.CurrentUserId = currentUser?.Id.ToString(); // Ensure this matches the type of your user ID

            // Populate other properties of viewModel as needed
            viewModel.Managers = await _calendarAppService.GetManagersAsync();

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AssignShift(DateTime date, int userId, int? year, int? month)
        {
            await _calendarAppService.AssignOrUpdateShiftAsync(date, userId);
            int redirectYear = year ?? date.Year;
            int redirectMonth = month ?? date.Month;

            return RedirectToAction(nameof(Index), new { year = redirectYear, month = redirectMonth });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteShift(int id, int? year, int? month)
        {
            await _calendarAppService.DeleteShiftAsync(id);

            // Redirect back to the calendar view with the appropriate year and month, if provided
            return RedirectToAction(nameof(Index), new { year, month });
        }

    }
}
