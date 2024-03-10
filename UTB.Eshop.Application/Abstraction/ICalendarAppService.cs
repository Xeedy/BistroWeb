using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface ICalendarAppService
    {
        Task<IEnumerable<Calendar>> GetAllCalendarsAsync();
        Task<Calendar> GetCalendarByIdAsync(int id);
        Task AddCalendarAsync(Calendar calendar);
        Task UpdateCalendarAsync(Calendar calendar);
        Task DeleteCalendarAsync(int id);

        Task<IEnumerable<Shift>> GetAllShiftsAsync();
        Task<Shift> GetShiftByIdAsync(int id);
        Task AddShiftAsync(Shift shift);
        Task UpdateShiftAsync(Shift shift);
        Task DeleteShiftAsync(int id);
    }
}
