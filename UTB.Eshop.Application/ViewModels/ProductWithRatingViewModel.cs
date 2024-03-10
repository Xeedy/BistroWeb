using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.ViewModels
{
    public class ProductWithRating
    {
        public string Name { get; set; }
        public double AverageRating { get; set; }
    }
}
