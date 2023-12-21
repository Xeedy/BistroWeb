using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Implementation.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BistroWeb.Domain.Entities
{
    public class Brewery : Entity<int>
    {
        [Required]
        [StringLength(70)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageSrc { get; set; }

        [NotMapped]
        [FileContent("image")]
        public IFormFile? Image { get; set; }
    }
}
