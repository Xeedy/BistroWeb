using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;

namespace BistroWeb.Application.Implementation
{
    public class HomeService : IHomeService
    {
        public CarouselProductViewModel GetIndexViewModel()
        {
            CarouselProductViewModel viewModel = new CarouselProductViewModel();
            viewModel.Products = DatabaseFake.Products;
            viewModel.Items = DatabaseFake.Items;

            // Explicitly cast IList<Brewery> to List<Brewery>
            viewModel.Breweries = (List<Brewery>)DatabaseFake.Brewery;

            return viewModel;
        }
    }
}
