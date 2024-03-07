using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Application.Abstraction;
using BistroWeb.Application.ViewModels;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BistroWeb.Application.Implementation
{
    public class TappedAppService : ITappedAppService
    {
        EshopDbContext _eshopDbContext;

        public TappedAppService(EshopDbContext eshopDbContext)
        {
            _eshopDbContext = eshopDbContext;
        }
        public Tapped GetTappedById(int id)
        {
            return _eshopDbContext.Tappeds.Find(id);
        }

        public IList<Tapped> Select()
        {
            return _eshopDbContext.Tappeds.ToList();
        }

        public async Task Create(Tapped tapped)
        {
            if (_eshopDbContext.Tappeds != null)
            {
                _eshopDbContext.Tappeds.Add(tapped);
                _eshopDbContext.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            bool deleted = false;

            Tapped? tapped
                = _eshopDbContext.Tappeds.FirstOrDefault(tapp => tapp.Id == id);

            if (tapped != null)
            {
                _eshopDbContext.Tappeds.Remove(tapped);
                _eshopDbContext.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        public async Task Edit(Tapped editedTapped)
        {
            // Fetch the existing product from the database
            var existingTapped = _eshopDbContext.Tappeds.FirstOrDefault(p => p.Id == editedTapped.Id);

            if (existingTapped != null)
            {
                // Update other properties
                existingTapped.Name = editedTapped.Name;
                existingTapped.Description = editedTapped.Description;
                existingTapped.MainPrice = editedTapped.MainPrice;
                existingTapped.OtherPrice = editedTapped.OtherPrice;


                // Set the BreweryId for the existing product
                existingTapped.BreweryId = editedTapped.BreweryId;
                existingTapped.TypeeId = editedTapped.TypeeId;


                // Save changes to the database
                await _eshopDbContext.SaveChangesAsync();
            }
        }
    }


}
