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
    public class BreweryAppService : IBreweryAppService
    {
        IFileUploadService _fileUploadService;
        EshopDbContext _eshopDbContext;

        public BreweryAppService(IFileUploadService fileUploadService, EshopDbContext eshopDbContext)
        {
            _fileUploadService = fileUploadService;
            _eshopDbContext = eshopDbContext;
        }
        public Brewery GetBreweryById(int id)
        {
            return _eshopDbContext.Brewery.Find(id);
        }
        public IEnumerable<Brewery> GetAllBreweries()
        {
            return _eshopDbContext.Brewery.ToList();
        }
        public IList<Brewery> Select()
        {
            return _eshopDbContext.Brewery.ToList();
        }

        public async Task Create(Brewery brewery)
        {
            string imageSrc = await _fileUploadService.FileUploadAsync(brewery.Image, Path.Combine("img", "brewery"));
            brewery.ImageSrc = imageSrc;

            if (_eshopDbContext.Brewery != null)
            {
                _eshopDbContext.Brewery.Add(brewery);
                _eshopDbContext.SaveChanges();
            }
        }
        
        public bool Delete(int id)
        {
            bool deleted = false;

            Brewery? brewery
                = _eshopDbContext.Brewery.FirstOrDefault(prod => prod.Id == id);

            if (brewery != null)
            {
                _eshopDbContext.Brewery.Remove(brewery);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }
            return deleted;
        }
        public async Task Edit(Brewery editedBrewery)
        {
            Brewery existingBrewery = _eshopDbContext.Brewery.Find(editedBrewery.Id);

            if (existingBrewery != null)
            {
                // Update the properties of the existing product with the edited values
                existingBrewery.Name = editedBrewery.Name;
                existingBrewery.Description = editedBrewery.Description;
  
                // ... (update other properties as needed)

                // If a new image is provided, upload and update the image source
                if (editedBrewery.Image != null)
                {
                    string newImageSrc = await _fileUploadService.FileUploadAsync(editedBrewery.Image, Path.Combine("img", "brewery"));
                    existingBrewery.ImageSrc = newImageSrc;
                }

                // Save the changes to the database
                _eshopDbContext.SaveChanges();
            }
        }
    }
}
