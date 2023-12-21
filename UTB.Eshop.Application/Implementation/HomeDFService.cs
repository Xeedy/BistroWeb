using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Database;
using BistroWeb.Domain.Entities;

public class HomeDFService : IHomeService
{
    public CarouselProductViewModel GetIndexViewModel()
    {
        CarouselProductViewModel viewModel = new CarouselProductViewModel();
        viewModel.Products = DatabaseFake.Products;
        viewModel.Items = DatabaseFake.Items;
        viewModel.Breweries = DatabaseFake.Brewery.ToList(); // Convert to list if needed
        return viewModel;
    }
}
