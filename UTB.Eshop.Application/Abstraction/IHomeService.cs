using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.ViewModels;

namespace BistroWeb.Application.Abstraction
{
    public interface IHomeService
    {
        CarouselProductViewModel GetIndexViewModel();
        public BreweryProductViewModel GetIndexViewModel2();
    }
}
