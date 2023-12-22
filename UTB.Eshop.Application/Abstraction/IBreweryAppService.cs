using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface IBreweryAppService
    {
        IList<Brewery> Select();
        Task Create(Brewery brewery);
        bool Delete(int id);
        Task Edit(Brewery editedBrewery);
        Brewery GetBreweryById(int id);
        IEnumerable<Brewery> GetBreweries();
    }
}