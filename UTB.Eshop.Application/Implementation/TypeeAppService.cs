using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;

namespace BistroWeb.Application.Implementation
{
    public class TypeeAppService : ITypeeAppService
    {
        EshopDbContext _eshopDbContext;

        public TypeeAppService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }
        public Typee GetTypeeById(int id)
        {
            return _eshopDbContext.Typees.Find(id);
        }

        public IList<Typee> Select()
        {
            return _eshopDbContext.Typees.ToList();
        }

        public async Task Create(Typee typee)
        {
            if (_eshopDbContext.Typees != null)
            {
                _eshopDbContext.Typees.Add(typee);
                _eshopDbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Typee? typee
                = _eshopDbContext.Typees.FirstOrDefault(prod => prod.Id == id);

            if (typee != null)
            {
                _eshopDbContext.Typees.Remove(typee);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }
            return deleted;
        }
        public async Task Edit(Typee editedTypee)
        {
            Typee existingTypee = _eshopDbContext.Typees.Find(editedTypee.Id);

            if (existingTypee != null)
            {
                // Update the properties of the existing product with the edited values
                existingTypee.Name = editedTypee.Name;
                existingTypee.Description = editedTypee.Description;
                
                // ... (update other properties as needed)

                // If a new image is provided, upload and update the image source

                // Save the changes to the database
                _eshopDbContext.SaveChanges();
            }
        }
    }
}
