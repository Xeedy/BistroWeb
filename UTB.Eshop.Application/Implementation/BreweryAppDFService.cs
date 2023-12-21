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
    public class BreweryAppDFService : IBreweryAppService
    {
        IFileUploadService _fileUploadService;
        EshopDbContext _eshopDbContext;
        public Brewery GetBreweryById(int id)
        {
            return _eshopDbContext.Brewery.Find(id);
        }
        public BreweryAppDFService(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        public IList<Brewery> Select()
        {
            return DatabaseFake.Brewery;
        }

        public async Task Create(Brewery brewery)
        {
            if(DatabaseFake.Brewery != null
                && DatabaseFake.Brewery.Count > 0)
            {
                brewery.Id = DatabaseFake.Brewery.Last().Id + 1;
            }
            else
                brewery.Id = 1;

            string imageSrc = await _fileUploadService.FileUploadAsync(brewery.Image, Path.Combine("img", "brewery"));
            brewery.ImageSrc = imageSrc;

            if (DatabaseFake.Brewery != null)
                DatabaseFake.Brewery.Add(brewery);
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Brewery? brewery
                = DatabaseFake.Brewery.FirstOrDefault(prod => prod.Id == id);

            if (brewery != null)
            {
                deleted = DatabaseFake.Brewery.Remove(brewery);
            }
            return deleted;
        }
        public async Task Edit(Brewery editedBrewery)
        {
            Brewery existingBrewery = _eshopDbContext.Brewery.Find(editedBrewery.Id);

            if (existingBrewery != null)
            {
                existingBrewery.Name = editedBrewery.Name;
                existingBrewery.Description = editedBrewery.Description;

                if (editedBrewery.Image != null)
                {
                    string newImageSrc = await _fileUploadService.FileUploadAsync(editedBrewery.Image, Path.Combine("img", "brewery"));
                    existingBrewery.ImageSrc = newImageSrc;
                }

                _eshopDbContext.SaveChanges();
            }
        }
    }
}
