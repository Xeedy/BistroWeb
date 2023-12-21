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
    public class MenuItemAppDFService : IMenuItemAppService
    {
        IFileUploadService _fileUploadService;
        EshopDbContext _eshopDbContext;
        public Item GetItemById(int id)
        {
            return _eshopDbContext.Items.Find(id);
        }
        public MenuItemAppDFService(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public IList<Item> Select()
        {
            return DatabaseFake.Items;
        }

        public async Task Create(Item item)
        {
            if(DatabaseFake.Items != null
                && DatabaseFake.Items.Count > 0)
            {
                item.Id = DatabaseFake.Items.Last().Id + 1;
            }
            else
                item.Id = 1;

            if (DatabaseFake.Items != null)
                DatabaseFake.Items.Add(item);
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Item? item
                = DatabaseFake.Items.FirstOrDefault(prod => prod.Id == id);

            if (item != null)
            {
                deleted = DatabaseFake.Items.Remove(item);
            }
            return deleted;
        }
        public async Task Edit(Item editedItem)
        {
            Item existingItem = _eshopDbContext.Items.Find(editedItem.Id);

            if (existingItem != null)
            {
                existingItem.Name = editedItem.Name;
                existingItem.Description = editedItem.Description;
                existingItem.Price = editedItem.Price;
                existingItem.Section = editedItem.Section;
                existingItem.Price2 = editedItem.Price2;
                // ... (update other properties as needed)


                _eshopDbContext.SaveChanges();
            }
        }
    }
}
