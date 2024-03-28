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
        public List<Tapped> Tapeeds { get; set; } = new List<Tapped>();
        public List<Typee> Typees { get; set; } = new List<Typee>();
 
        public IList<Brewery> Brewery { get; set; }
        [Required(ErrorMessage = "Please select a brewery")]
        public int SelectedBreweryId { get; set; }
        public IList<Typee> Typee { get; set; }
        public int SelectedTypeeId { get; set;}
    }
}
