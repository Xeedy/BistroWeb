﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using BistroWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BistroWeb.Application.Implementation
{
    
    public class CalendarAppService : ICalendarAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly EshopDbContext _context;

        public CalendarAppService(EshopDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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
            if (shift.UserId == null)
            {
                // Handle the case where UserId is null, maybe log an error or throw an exception
                throw new InvalidOperationException("UserId cannot be null when adding a shift.");
            }

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
        public async Task<CalendarViewModel> GetCalendarViewModelAsync(int year, int month)
        {
            // Assume existing logic to define the start and end of the month is here
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Fetch shifts within the month
            var shifts = await _context.Shifts
                .Where(s => s.StartDate >= firstDayOfMonth && s.EndDate <= lastDayOfMonth)
                .ToListAsync();

            var viewModel = new CalendarViewModel
            {
                // Initialize or set other properties of viewModel as needed
                CurrentYear = year,
                CurrentMonth = month,
                // Initialize ShiftAssignments as an empty dictionary
                ShiftAssignments = new Dictionary<DateTime, List<ShiftAssignmentViewModel>>()
            };

            // Transform shifts to ShiftAssignmentViewModels
            foreach (var shift in shifts)
            {
                var user = await _userManager.FindByIdAsync(shift.UserId.ToString());
                if (user != null)
                {
                    var assignment = new ShiftAssignmentViewModel
                    {
                        ShiftId = shift.Id,
                        User = user
                    };

                    // Assuming you have a way to associate shifts with dates, e.g., using shift.StartDate
                    if (!viewModel.ShiftAssignments.ContainsKey(shift.StartDate))
                    {
                        viewModel.ShiftAssignments[shift.StartDate] = new List<ShiftAssignmentViewModel>();
                    }
                    viewModel.ShiftAssignments[shift.StartDate].Add(assignment);
                }
            }

            return viewModel;
        }

        public async Task<IEnumerable<User>> GetManagersAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("Manager");
            return users;
        }
        public async Task AssignOrUpdateShiftAsync(DateTime date, int userId)
        {
            var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.StartDate.Date == date.Date);

            if (shift != null)
            {
                // If a shift for this date exists, update its UserId
                shift.UserId = userId;
            }
            else
            {
                // If no shift exists for this date, create a new one
                shift = new Shift
                {
                    StartDate = date,
                    EndDate = date, // Assuming end date is the same for simplicity
                    UserId = userId
                };
                _context.Shifts.Add(shift);
            }

            await _context.SaveChangesAsync();
        }
    }
}
