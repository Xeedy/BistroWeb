using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface IMissingAppService
    {
        IList<Missing> Select();
        Task Create(Missing missing);
        bool Delete(int id);
        Task Edit(Missing editedMissing);
        Missing GetMissingById(int id);
    }
}