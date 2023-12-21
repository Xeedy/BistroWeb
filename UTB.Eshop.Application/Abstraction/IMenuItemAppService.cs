using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface IMenuItemAppService
    {
        IList<Item> Select();
        Task Create(Item item);
        bool Delete(int id);
        Task Edit(Item editedItem);
        Item GetItemById(int id); // Add this method
    }
}
