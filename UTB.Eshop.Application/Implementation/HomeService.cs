using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;

namespace BistroWeb.Application.Implementation
{
    public class HomeService : IHomeService
    {
        EshopDbContext _eshopDbContext;
        public HomeService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }
        public CarouselProductViewModel GetIndexViewModel()
        {
            CarouselProductViewModel viewModel = new CarouselProductViewModel();
            viewModel.Products = _eshopDbContext.Products.ToList();
            viewModel.Items = _eshopDbContext.Items.ToList();
            viewModel.Brewery = _eshopDbContext.Brewery.ToList();
            return viewModel;
        }
        public BreweryProductViewModel GetIndexViewModel2()
        {
            BreweryProductViewModel viewModel = new BreweryProductViewModel();
            viewModel.Products = _eshopDbContext.Products.ToList();
            viewModel.Breweries = _eshopDbContext.Brewery.ToList();

            return viewModel;
        }
    }
}
