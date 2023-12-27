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
    public class MenuItemAppService : IMenuItemAppService
    {
        IFileUploadService _fileUploadService;
        EshopDbContext _eshopDbContext;

        public MenuItemAppService(IFileUploadService fileUploadService, EshopDbContext eshopDbContext)
        {
            _fileUploadService = fileUploadService;
            _eshopDbContext = eshopDbContext;
        }
        public Item GetItemById(int id)
        {
            return _eshopDbContext.Items.Find(id);
        }

        public IList<Item> Select()
        {
            return _eshopDbContext.Items.ToList();
        }

        public async Task Create(Item item)
        {
            if (_eshopDbContext.Items != null)
            {
                _eshopDbContext.Items.Add(item);
                _eshopDbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Item? item
                = _eshopDbContext.Items.FirstOrDefault(prod => prod.Id == id);

            if (item != null)
            {
                _eshopDbContext.Items.Remove(item);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }
            return deleted;
        }
        public async Task Edit(Item editedItem)
        {
            Item existingItem = _eshopDbContext.Items.Find(editedItem.Id);

            if (existingItem != null)
            {
                // Update the properties of the existing product with the edited values
                existingItem.Name = editedItem.Name;
                existingItem.Description = editedItem.Description;
                existingItem.Price = editedItem.Price;
                existingItem.Section = editedItem.Section;
                existingItem.Price2 = editedItem.Price2;
                // ... (update other properties as needed)

                // If a new image is provided, upload and update the image source

                // Save the changes to the database
                _eshopDbContext.SaveChanges();
            }
        }
    }
}
