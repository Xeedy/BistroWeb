using BistroWeb.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Application.ViewModels
{
    public class ShiftAssignmentViewModel // Changed from internal to public if needed
    {
        public int ShiftId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; } // Adding Date property
    }
}
