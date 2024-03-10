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
        public List<Typee> Typees { get; set; }
        public int? UserRating { get; set; }
        public ProductViewModel()
        {
            Product = new List<Product>();
            Breweries = new List<Brewery>();
            Typees = new List<Typee>();
        }
    }

}