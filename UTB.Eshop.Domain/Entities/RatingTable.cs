using BistroWeb.Domain.Entities.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BistroWeb.Domain.Entities
{
    public class Rating
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }
        [NotMapped] // This tells EF Core to ignore the User property
        public virtual IUser User { get; set; }

        public int RatingValue { get; set; }
        public static double CalculateAverageRating(IEnumerable<Rating> ratings)
        {
            // Check if there are any ratings for the product
            if (ratings == null || !ratings.Any())
            {
                return 0; // If no ratings, return 0
            }

            // Calculate the average rating
            double averageRating = ratings.Average(r => r.RatingValue);
            return averageRating;
        }
    }
}
