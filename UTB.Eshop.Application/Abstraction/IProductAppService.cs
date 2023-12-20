using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;

namespace BistroWeb.Application.Abstraction
{
    public interface IProductAppService
    {
        IList<Product> Select();
        Task Create(Product product);
        bool Delete(int id);
        Task Edit(Product editedProduct);
        Product GetProductById(int id); // Add this method
    }
}
