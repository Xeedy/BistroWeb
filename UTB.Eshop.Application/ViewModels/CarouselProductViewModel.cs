using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.ViewModels
{
    public class CarouselProductViewModel
    {
        public IList<Carousel> Carousels { get; set; }
        public IList<Product> Products { get; set; }
    }
}
