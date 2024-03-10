using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using BistroWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BistroWeb.Application.Implementation
{
    public class RatingTableAppService : IRatingTableAppService
    {
        IFileUploadService _fileUploadService;
        EshopDbContext _eshopDbContext;

        public RatingTableAppService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }

        public Rating GetRatingById(int id) // Corrected method name
        {
            return _eshopDbContext.Ratings.FirstOrDefault(r => r.Id == id);
        }

        public IList<Rating> Select()
        {
            return _eshopDbContext.Ratings.ToList();
        }

        public async Task Create(Rating rating)
        {
            if (_eshopDbContext.Ratings != null)
            {
                _eshopDbContext.Ratings.Add(rating);
                _eshopDbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Rating? rating
                = _eshopDbContext.Ratings.FirstOrDefault(prod => prod.Id == id);

            if (rating != null)
            {
                _eshopDbContext.Ratings.Remove(rating);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        public async Task Edit(Rating editedRating)
        {
            // Fetch the existing product from the database
            var existingRating = _eshopDbContext.Ratings.FirstOrDefault(p => p.Id == editedRating.Id);

            if (existingRating != null)
            {
                // Update other properties
                existingRating.RatingValue = editedRating.RatingValue;

                // Save changes to the database
                await _eshopDbContext.SaveChangesAsync();
            }
        }
    }


}
