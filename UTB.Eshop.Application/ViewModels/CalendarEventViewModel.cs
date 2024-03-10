using BistroWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Application.ViewModels
{
    public class CalendarEventViewModel
    {
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public IEnumerable<CalendarEvent> Events { get; set; }

        public int NumberOfDaysInMonth => DateTime.DaysInMonth(CurrentYear, CurrentMonth);
        public DateTime FirstDayOfMonth => new DateTime(CurrentYear, CurrentMonth, 1);
        public DayOfWeek FirstDayOfWeek => FirstDayOfMonth.DayOfWeek;
        public int DaysInPreviousMonth => DateTime.DaysInMonth(CurrentYear, CurrentMonth - 1);

        // Add properties or methods here as needed
    }
}
