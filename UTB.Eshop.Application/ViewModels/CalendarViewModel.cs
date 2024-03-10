using BistroWeb.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Application.ViewModels
{
    public class CalendarViewModel
    {
        public IEnumerable<Calendar> Calendars { get; set; }
        public IEnumerable<Shift> Shifts { get; set; }
        public int NumberOfDaysInMonth { get; set; }
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
    }
}
