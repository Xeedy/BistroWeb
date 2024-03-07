using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.ViewModels
{
    public class TappedViewModel
    {
        public List<Product> Tapped { get; set; }
        public List<Brewery> Breweries { get; set; }
        public List<Typee> Typees { get; set; }
        public TappedViewModel()
        {
            // Initialize Breweries to an empty list
            Breweries = new List<Brewery>();
            Typees = new List<Typee>();
        }
    }

}