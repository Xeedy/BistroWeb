using BistroWeb.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Domain.Entities
{
    internal class Shifts
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } // For example, "Morning Shift", "Evening Shift"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; } // Foreign key to the User table
        public virtual IUser User { get; set; } // Navigation property
    }
}
