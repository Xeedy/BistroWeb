using BistroWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Application.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Shift> UpcomingShifts { get; set; }
    }
}

