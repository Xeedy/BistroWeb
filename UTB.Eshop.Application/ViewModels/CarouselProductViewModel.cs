using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.ViewModels
{
    public class CarouselProductViewModel
    {
        public IList<Carousel> Carousels { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public IList<Brewery> Brewery { get; set; }
        [Required(ErrorMessage = "Please select a brewery")]
        public int SelectedBreweryId { get; set; }
    }
}
