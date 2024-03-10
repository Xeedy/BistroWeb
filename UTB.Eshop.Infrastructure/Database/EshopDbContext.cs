using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Identity;
using System.Diagnostics;

namespace BistroWeb.Infrastructure.Database
{
    public class EshopDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Missing> Missings { get; set; }
        public DbSet<Tapped> Tappeds { get; set; }
        public DbSet<Typee> Typees { get; set; }
        public DbSet<Rating> Ratings { get; set; }


        public EshopDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Debug.WriteLine("OnModelCreating: Seeding data...");

            DatabaseInit dbInit = new DatabaseInit();

            // Get single instances of Brewery and Typee
            IList<Brewery> brewery = dbInit.GetBrewery();
            IList<Typee> typee = dbInit.GetTypee();


            // Seed Products using Brewery and Typee
            modelBuilder.Entity<Product>().HasData(dbInit.GetProducts(brewery, typee));
            modelBuilder.Entity<Rating>().Ignore(r => r.User);
            // Seed other entities
            modelBuilder.Entity<Tapped>().HasData(dbInit.GetTappeds(brewery, typee));
            modelBuilder.Entity<Item>().HasData(dbInit.GetItems());
            modelBuilder.Entity<Carousel>().HasData(dbInit.GetCarousels());
            modelBuilder.Entity<Brewery>().HasData(brewery);
            modelBuilder.Entity<Typee>().HasData(typee);
            modelBuilder.Entity<Missing>().HasData(dbInit.GetMissing());

            // Identity - User and Role initialization
            // Roles must be added first
            modelBuilder.Entity<Role>().HasData(dbInit.CreateRoles());

            // Then, create users
            (User admin, List<IdentityUserRole<int>> adminUserRoles) = dbInit.CreateAdminWithRoles();
            (User manager, List<IdentityUserRole<int>> managerUserRoles) = dbInit.CreateManagerWithRoles();

            // Add users to the table
            modelBuilder.Entity<User>().HasData(admin, manager);

            // Finally, connect the users with the roles
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminUserRoles);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(managerUserRoles);
        }

    }
}
