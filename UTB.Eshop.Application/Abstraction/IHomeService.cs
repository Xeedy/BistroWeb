using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface IHomeService
    {
        CarouselProductViewModel GetIndexViewModel();
        IEnumerable<Product> GetProducts();
        MenuItemViewModel GetIndexViewModel2();
    }
}
