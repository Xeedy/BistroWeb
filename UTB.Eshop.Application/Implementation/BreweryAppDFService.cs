using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BistroWeb.Application.Implementation
{
    internal class BreweryAppDFService : IBreweryAppService
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly BreweryDbService _breweryDbService;

        public BreweryAppDFService(IFileUploadService fileUploadService, BreweryDbService breweryDbService)
        {
            _fileUploadService = fileUploadService;
            _breweryDbService = breweryDbService;
        }

        public IList<Brewery> Select()
        {
            // Use the appropriate method from BreweryDbService to get breweries
            return _breweryDbService.GetAllBreweries().ToList();
        }

        public async Task Create(Brewery brewery)
        {
            // Set the Id based on the existing breweries in the database
            brewery.Id = _breweryDbService.GetNextBreweryId();

            // Upload and set the image source
            string imageSrc = await _fileUploadService.FileUploadAsync(brewery.Image, Path.Combine("img", "brewery"));
            brewery.ImageSrc = imageSrc;

            // Use the appropriate method from BreweryDbService to add the brewery
            _breweryDbService.AddBrewery(brewery);
        }

        public RedirectToRouteResult Delete(int id)
        {
            bool deleted = _breweryDbService.Delete(id);

            if (deleted)
            {
                // Assuming "Index" is the action method and "Brewery" is the controller
                return RedirectToAction("Index", "Brewery");
            }
            else
            {
                return RedirectToRoute("NotFound"); // Replace "NotFound" with your route name or URL
            }
        }

        public async Task Edit(Brewery editedBrewery)
        {
            // Use the appropriate method from BreweryDbService to edit the brewery
            await _breweryDbService.EditBrewery(editedBrewery);
        }
    }

}
