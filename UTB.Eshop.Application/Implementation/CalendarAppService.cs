using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;

namespace BistroWeb.Application.Implementation
{
    public class CalendarAppService : ICalendarAppService
    {
        private readonly EshopDbContext _context;
        public CalendarAppService(EshopDbContext context)
        {
            _context = context;
        }
        public CalendarEventViewModel GetCalendarEvents(int year, int month)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var events = _context.CalendarEvents
                .Where(e => e.StartDate >= firstDayOfMonth && e.EndDate <= lastDayOfMonth)
                .ToList();

            var viewModel = new CalendarEventViewModel
            {
                CurrentMonth = month,
                CurrentYear = year,
                Events = events
            };

            return viewModel;
        }
        public async Task UpdateEventAsync(CalendarEvent calendarEvent)
        {
            var existingEvent = await _context.CalendarEvents.FindAsync(calendarEvent.Id);
            if (existingEvent != null)
            {
                // Update properties as necessary
                existingEvent.Title = calendarEvent.Title;
                existingEvent.StartDate = calendarEvent.StartDate;
                existingEvent.EndDate = calendarEvent.EndDate;
                existingEvent.Description = calendarEvent.Description;
                // Add more properties to update as needed

                await _context.SaveChangesAsync();
            }
            else
            {
                // Handle the case where the event doesn't exist, e.g., throw an exception or log a warning
            }
        }
    }
}
