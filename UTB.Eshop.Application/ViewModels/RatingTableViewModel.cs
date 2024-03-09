using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;
using BistroWeb.Domain.Entities.Interfaces;

namespace BistroWeb.Application.ViewModels;

public class RatingTableViewModel
{
    public List<Rating> Rating { get; set; }
    public List<Product> Products { get; set; }
    public List<IUser> Users { get; set; }
    public RatingTableViewModel()
    {
        // Initialize Breweries to an empty list
        Products = new List<Product>();
        Users = new List<IUser>();
    }
}