using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Identity;
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
        public IEnumerable<User> Managers { get; set; }
        public int? UserId { get; set; }
        public string CurrentUserId { get; set; }
        public int PreviousMonth { get; set; }
        public int PreviousMonthYear { get; set; }
        public int NextMonth { get; set; }
        public int NextMonthYear { get; set; }
        public Dictionary<DateTime, List<ShiftAssignmentViewModel>> ShiftAssignments { get; set; } = new Dictionary<DateTime, List<ShiftAssignmentViewModel>>();

    }
}
