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
    public class MissingAppService : IMissingAppService
    {
        EshopDbContext _eshopDbContext;

        public MissingAppService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }
        public Missing GetMissingById(int id)
        {
            return _eshopDbContext.Missings.Find(id);
        }

        public IList<Missing> Select()
        {
            return _eshopDbContext.Missings.ToList();
        }

        public async Task Create(Missing missing)
        {
            if (_eshopDbContext.Missings != null)
            {
                _eshopDbContext.Missings.Add(missing);
                _eshopDbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Missing? missing
                = _eshopDbContext.Missings.FirstOrDefault(prod => prod.Id == id);

            if (missing != null)
            {
                _eshopDbContext.Missings.Remove(missing);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }
            return deleted;
        }
        public async Task Edit(Missing editedMissing)
        {
            Missing existingMissing = _eshopDbContext.Missings.Find(editedMissing.Id);

            if (existingMissing != null)
            {
                // Update the properties of the existing product with the edited values
                existingMissing.Name = editedMissing.Name;
                existingMissing.Description = editedMissing.Description;
                // ... (update other properties as needed)

                // If a new image is provided, upload and update the image source

                // Save the changes to the database
                _eshopDbContext.SaveChanges();
            }
        }
    }
}
