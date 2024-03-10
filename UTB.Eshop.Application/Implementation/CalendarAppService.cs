using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BistroWeb.Application.Implementation
{
    public class CalendarAppService : ICalendarAppService
    {
        private readonly EshopDbContext _context;

        public CalendarAppService(EshopDbContext context)
        {
            _context = context;
        }

        // Calendar CRUD operations

        public async Task<IEnumerable<Calendar>> GetAllCalendarsAsync()
        {
            return await _context.Calendars.ToListAsync();
        }

        public async Task<Calendar> GetCalendarByIdAsync(int id)
        {
            return await _context.Calendars.FindAsync(id);
        }

        public async Task AddCalendarAsync(Calendar calendar)
        {
            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCalendarAsync(Calendar calendar)
        {
            _context.Entry(calendar).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCalendarAsync(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);
            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();
        }

        // Shift CRUD operations

        public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
        {
            return await _context.Shifts.ToListAsync();
        }

        public async Task<Shift> GetShiftByIdAsync(int id)
        {
            return await _context.Shifts.FindAsync(id);
        }

        public async Task AddShiftAsync(Shift shift)
        {
            _context.Shifts.Add(shift);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShiftAsync(Shift shift)
        {
            _context.Entry(shift).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShiftAsync(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            _context.Shifts.Remove(shift);
            await _context.SaveChangesAsync();
        }
    }
}
