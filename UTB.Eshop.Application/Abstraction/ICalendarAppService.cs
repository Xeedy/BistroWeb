using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface ICalendarAppService
    {
        CalendarEventViewModel GetCalendarEvents(int year, int month);
        Task UpdateEventAsync(CalendarEvent calendarEvent);
    }
}
