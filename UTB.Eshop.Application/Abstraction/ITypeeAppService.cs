using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface ITypeeAppService
    {
        IList<Typee> Select();
        Task Create(Typee Typee);
        bool Delete(int id);
        Task Edit(Typee editedTypee);
        Typee GetTypeeById(int id); // Add this method
    }
}
