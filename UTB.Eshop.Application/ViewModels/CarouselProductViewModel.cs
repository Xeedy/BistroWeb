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
        public IList<Product> Products { get; set; }
        public IList<Item> Items { get; set; }
        public IList<Brewery> Brewery { get; set; }
    }
}
