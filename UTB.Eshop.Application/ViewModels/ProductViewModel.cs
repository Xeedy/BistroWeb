using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Brewery> Breweries { get; set; }
    }

}