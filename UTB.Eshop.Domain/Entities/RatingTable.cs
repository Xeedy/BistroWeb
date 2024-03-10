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
    }
}
