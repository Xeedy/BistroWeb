﻿using BistroWeb.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Domain.Entities
{
    public class Shift
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; } // Foreign key to the User table
        public virtual IUser User { get; set; } // Navigation property
    }
}
