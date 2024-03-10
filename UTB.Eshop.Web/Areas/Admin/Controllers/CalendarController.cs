using BistroWeb.Application.Abstraction;
using BistroWeb.Application.Implementation;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;

namespace BistroWeb.Web.Areas.Admin.Controllers
{
    public class CalendarController : Controller
    {
        ICalendarAppService _calendarAppService;
        EshopDbContext _eshopDbContext;
        public CalendarController(ICalendarAppService calendarAppService, EshopDbContext eshopDbContext)
        {
            _calendarAppService = calendarAppService;
            _eshopDbContext = eshopDbContext;
        }
        public IActionResult Index(int? year, int? month)
        {
            var now = DateTime.Now;
            var viewModel = _calendarAppService.GetCalendarEvents(year ?? now.Year, month ?? now.Month);
            return View(viewModel);
        }
    }
}
