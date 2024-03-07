using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface ITappedAppService
    {
        IList<Tapped> Select();
        Task Create(Tapped tapped);
        bool Delete(int id);
        Task Edit(Tapped editedTapped);
        Tapped GetTappedById(int id);
    }
}