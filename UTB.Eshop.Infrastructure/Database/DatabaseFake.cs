using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Infrastructure.Database
{
    public class DatabaseFake
    {
        public static IList<Product> Products { get; set; }
        public static IList<Item> Items { get; set; }
        public static IList<Carousel> Carousels { get; set; }
        public static IList<Brewery> Brewery { get; set; }

        static DatabaseFake()
        {
            DatabaseInit dbInit = new DatabaseInit();
            IList<Brewery> breweries = dbInit.GetBrewery();  // Get breweries first
            Products = dbInit.GetProducts(breweries);  // Pass the list of breweries to GetProducts
            Items = dbInit.GetItems();
            Carousels = dbInit.GetCarousels();
            Brewery = breweries;  // Store the list of breweries
        }
    }
}
