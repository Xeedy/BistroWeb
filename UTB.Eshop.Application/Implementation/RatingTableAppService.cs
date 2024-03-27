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
        public Rating GetRatingById(int id)
        {
            return _eshopDbContext.Ratings.FirstOrDefault(r => r.Id == id);
        }
        // In your RatingTableAppService class that implements IRatingTableAppService
        public async Task<int?> GetUserRatingForProduct(int productId, string userId)
        {
            int userIdInt = int.Parse(userId); // This is risky if userId isn't always an integer
            var rating = await _eshopDbContext.Ratings
                .Where(r => r.ProductId == productId && r.UserId == userIdInt)
                .Select(r => r.RatingValue)
                .FirstOrDefaultAsync();

            return rating;
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
        public async Task CreateOrUpdateRating(Rating rating)
        {
            // Check if the rating already exists
            var existingRating = await _eshopDbContext.Ratings
                .FirstOrDefaultAsync(r => r.ProductId == rating.ProductId && r.UserId == rating.UserId);

            if (existingRating != null)
            {
                // Update existing rating
                existingRating.RatingValue = rating.RatingValue;
            }
            else
            {
                // Create new rating
                _eshopDbContext.Ratings.Add(rating);
            }

            await _eshopDbContext.SaveChangesAsync();
        }
        public async Task<double> GetAverageRatingForProductAsync(int productId)
        {
            var ratingsForProduct = await _eshopDbContext.Ratings
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            double averageRating = ratingsForProduct.Any() ? ratingsForProduct.Average(r => r.RatingValue) : 0;
            return averageRating;
        }
        public async Task<List<Rating>> GetRatingsForProductAsync(int productId)
        {
            // Retrieve all ratings for the specified product
            var ratingsForProduct = await _eshopDbContext.Ratings
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            return ratingsForProduct;
        }
        public async Task<List<Rating>> GetRatingsAsync()
        {
            // Retrieve all ratings from the database
            return await _eshopDbContext.Ratings.ToListAsync();
        }
        public string GetProductNameById(int productId)
        {
            // Retrieve the name of the product by its ID
            // Assuming Product has a Name property
            var product = _eshopDbContext.Products.FirstOrDefault(p => p.Id == productId);
            return product?.Name ?? "Unknown"; // If the product is null, return "Unknown"
        }
        // In your IRatingTableAppService interface

        // Implementation in RatingTableAppService
        public async Task<List<Rating>> GetRatingsByUserIdAsync(string userId)
        {
            return await _eshopDbContext.Ratings
                .Where(r => r.UserId.ToString() == userId) // Adjust according to your UserId type
                .Include(r => r.Product) // Assuming you have a navigation property to Product
                .ToListAsync();
        }
    }
   



}
