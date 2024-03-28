using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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
            viewModel.Products = _eshopDbContext.Products.Include(p => p.Breweries).Include(p => p.Typees).ToList();
            viewModel.Brewery = _eshopDbContext.Breweries.ToList();
            viewModel.Carousels = _eshopDbContext.Carousels.ToList();
            viewModel.Tapeeds = _eshopDbContext.Tappeds.Include(p => p.Breweries).Include(p => p.Typees).ToList();
            viewModel.Typee = _eshopDbContext.Typees.ToList();
            return viewModel;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _eshopDbContext.Products.Include(p => p.Breweries).ToList();
        }
        public IEnumerable<Tapped> GetTappeds()
        {
            return _eshopDbContext.Tappeds.Include(p => p.Breweries).ToList();
            return _eshopDbContext.Tappeds.Include(p => p.Typees).ToList();

        }
        public MenuItemViewModel GetIndexViewModel2()
        {
            MenuItemViewModel viewModel = new MenuItemViewModel();
            viewModel.Items = _eshopDbContext.Items.ToList();
            return viewModel;
        }
    }
}
