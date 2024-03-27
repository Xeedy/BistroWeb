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
            viewModel.Products = _eshopDbContext.Products.ToList();
            viewModel.Brewery = _eshopDbContext.Breweries.ToList();
            viewModel.Carousels = _eshopDbContext.Carousels.ToList();
            return viewModel;
        }
        public IEnumerable<Product> GetProducts()
        {
            return _eshopDbContext.Products.Include(p => p.Breweries).ToList();
        }
        public MenuItemViewModel GetIndexViewModel2()
        {
            MenuItemViewModel viewModel = new MenuItemViewModel();
            viewModel.Items = _eshopDbContext.Items.ToList();
            return viewModel;
        }
    }
}
