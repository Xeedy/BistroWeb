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
    internal class BreweryDbService
    {
        private readonly EshopDbContext _eshopDbContext;

        public BreweryDbService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }
        public IEnumerable<Brewery> GetAllBreweries()
        {
            return _eshopDbContext.Breweries.ToList();
        }
        
    }
}
