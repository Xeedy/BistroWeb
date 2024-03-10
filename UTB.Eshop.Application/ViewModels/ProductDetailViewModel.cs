using BistroWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Application.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public int? UserRating { get; set; }
    }
}

