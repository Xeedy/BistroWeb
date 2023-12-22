using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.ViewModels
{
    public class BreweryProductViewModel
    {
        public IList<Product> Products { get; set; }
        public IList<Brewery> Breweries { get; set; }

        public BreweryProductViewModel()
        {
            // Ensure Breweries is initialized in the constructor if needed.
            Breweries = new List<Brewery>();
        }
    }
}