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
        public virtual IUser User { get; set; } // Use IUser interface instead of User class

        public int RatingValue { get; set; }
    }
}
