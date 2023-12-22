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
        private readonly IFileUploadService _fileUploadService;
        private readonly EshopDbContext _eshopDbContext;

        public BreweryAppService(IFileUploadService fileUploadService, EshopDbContext eshopDbContext)
        {
            _fileUploadService = fileUploadService;
            _eshopDbContext = eshopDbContext;
        }
        public Brewery GetBreweryById(int id)
        {
            return _eshopDbContext.Breweries.Find(id);
        }
        public IEnumerable<Brewery> GetAllBreweries()
        {
            return _eshopDbContext.Breweries.ToList();
        }
        public IList<Brewery> Select()
        {
            return _eshopDbContext.Breweries.ToList();
        }

        public async Task Create(Brewery brewery)
        {
            string imageSrc = await _fileUploadService.FileUploadAsync(brewery.Image, Path.Combine("img", "brewery"));
            brewery.ImageSrc = imageSrc;

            if (!_eshopDbContext.Breweries.Any(b => b.Id == brewery.Id))
            {
                _eshopDbContext.Breweries.Add(brewery);
                await _eshopDbContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Brewery with the same ID already exists");
            }
        }
        public bool Delete(int id)
        {
            bool deleted = false;

            Brewery? brewery = _eshopDbContext.Breweries.FirstOrDefault(prod => prod.Id == id);

            if (brewery != null)
            {
                _eshopDbContext.Breweries.Remove(brewery);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }

            return deleted;
        }
        public async Task Edit(Brewery editedBrewery)
        {
            // Retrieve the existing brewery from the database
            Brewery existingBrewery = _eshopDbContext.Breweries.Find(editedBrewery.Id);

            if (existingBrewery != null)
            {
                // Update other properties
                existingBrewery.Name = editedBrewery.Name;
                existingBrewery.Description = editedBrewery.Description;

                // Check if a new image is provided
                if (editedBrewery.Image != null)
                {
                    // Upload and update the image source
                    string newImageSrc = await _fileUploadService.FileUploadAsync(editedBrewery.Image, Path.Combine("img", "brewery"));
                    existingBrewery.ImageSrc = newImageSrc;
                }

                // Save the changes to the database
                await _eshopDbContext.SaveChangesAsync();
            }
        }
    }

   
}
