using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BistroWeb.Application.Implementation
{
    public class BreweryAppService : IBreweryAppService
    {
        private readonly EshopDbContext _eshopDbContext;

        public BreweryAppService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }

        // Implement IBreweryAppService methods here
        public IList<Brewery> Select()
        {
            // Your implementation to retrieve breweries from the database
            return _eshopDbContext.Breweries.ToList();
        }

        public async Task Create(Brewery brewery)
        {
            // Your implementation to create a new brewery in the database
            _eshopDbContext.Breweries.Add(brewery);
            await _eshopDbContext.SaveChangesAsync();
        }

        public bool Delete(int id)
        {
            // Your implementation to delete a brewery from the database
            var brewery = _eshopDbContext.Breweries.Find(id);
            if (brewery != null)
            {
                _eshopDbContext.Breweries.Remove(brewery);
                _eshopDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task Edit(Brewery brewery)
        {
            // Your implementation to edit a brewery in the database
            _eshopDbContext.Entry(brewery).State = EntityState.Modified;
            await _eshopDbContext.SaveChangesAsync();
        }

        public Brewery GetBreweryById(int id)
        {
            // Your implementation to get a brewery by ID from the database
            return _eshopDbContext.Breweries.Find(id);
        }
        public IList<Brewery> GetBreweries()
        {
            // Your implementation to get all breweries from the database
            return _eshopDbContext.Breweries.ToList();
        }
    }
}
