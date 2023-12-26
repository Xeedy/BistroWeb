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
        public List<Product> Product { get; set; }
        public List<Brewery> Breweries { get; set; }
        public ProductViewModel()
        {
            // Initialize Breweries to an empty list
            Breweries = new List<Brewery>();
        }
    }

}