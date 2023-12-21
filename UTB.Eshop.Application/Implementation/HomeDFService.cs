using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Database;

namespace BistroWeb.Application.Implementation
{
    public class HomeDFService : IHomeService
    {
        public CarouselProductViewModel GetIndexViewModel()
        {
            CarouselProductViewModel viewModel = new CarouselProductViewModel();
            viewModel.Products = DatabaseFake.Products;
            viewModel.Items = DatabaseFake.Items;
            viewModel.Brewery = DatabaseFake.Brewery;
            return viewModel;
        }
    }
}
