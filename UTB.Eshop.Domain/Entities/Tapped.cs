﻿using Microsoft.AspNetCore.Http;
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
    public class Tapped
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [StringLength(90)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double MainPrice { get; set; }
        public double OtherPrice { get; set; }
        [ForeignKey("Breweries")]
        public int? BreweryId { get; set; }
        public virtual Brewery Breweries { get; set; }
        [ForeignKey("Typees")]
        public int? TypeeId { get; set; }
        public virtual Typee Typees { get; set; }

    }
}