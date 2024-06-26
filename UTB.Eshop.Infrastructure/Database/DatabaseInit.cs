﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Identity;

namespace BistroWeb.Infrastructure.Database
{
    internal class DatabaseInit
    {
        public IList<Product> GetProducts()
        {
            IList<Product> products = new List<Product>();

            products.Add(new Product
            {
                Id = 1,
                Name = "Test",
                Brewery = "Testovaci pivovar",
                Description = "Testovací sada",
                Price = 999,
                ImageSrc = "/img/products/produkty-01.jpg"
            });

            return products;
        }
        public IList<Item> GetItems()
        {
            IList<Item> items = new List<Item>();

            items.Add(new Item
            {
                Id = 1,
                Name = "Test",
                Description = "Testovací sada",
                Price = 999,
                Section = "",
                Price2 = 0
            }); ;

            return items;
        }
        public IList<Brewery> GetBrewery()
        {
            IList<Brewery> brewery = new List<Brewery>();

            brewery.Add(new Brewery
            {
                Id = 1,
                Name = "Testovci pivovar",
                Description = "Testovací sada",
                ImageSrc = "/img/products/produkty-01.jpg"
            }); ;

            return brewery;
        }

        public IList<Carousel> GetCarousels()
        {
            IList<Carousel> carousels = new List<Carousel>();

            carousels.Add(new Carousel()
            {
                Id = 1,
                ImageSrc = "/img/carousel/information-technology-specialist.jpg",
                ImageAlt = "First slide"
            });

            carousels.Add(new Carousel()
            {
                Id = 2,
                ImageSrc = "/img/carousel/Information-Technology-1-1.jpg",
                ImageAlt = "Second slide"
            });

            carousels.Add(new Carousel()
            {
                Id = 3,
                ImageSrc = "/img/carousel/itec-index-banner.jpg",
                ImageAlt = "Third slide"
            });

            return carousels;
        }


        //for Identity tables


        public List<Role> CreateRoles()
        {
            List<Role> roles = new List<Role>();

            Role roleAdmin = new Role()
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "9cf14c2c-19e7-40d6-b744-8917505c3106"
            };

            Role roleManager = new Role()
            {
                Id = 2,
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = "be0efcde-9d0a-461d-8eb6-444b043d6660"
            };

            Role roleCustomer = new Role()
            {
                Id = 3,
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = "29dafca7-cd20-4cd9-a3dd-4779d7bac3ee"
            };


            roles.Add(roleAdmin);
            roles.Add(roleManager);
            roles.Add(roleCustomer);

            return roles;
        }


        public (User, List<IdentityUserRole<int>>) CreateAdminWithRoles()
        {
            User admin = new User()
            {
                Id = 1,
                FirstName = "Michal",
                LastName = "Těšík",
                UserName = "Xeedy",
                NormalizedUserName = "XEEDY",
                Email = "michal.tesik1@gmail.com",
                NormalizedEmail = "MICHAL.TESIK1@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEM9O98Suoh2o2JOK1ZOJScgOfQ21odn/k6EYUpGWnrbevCaBFFXrNL7JZxHNczhh/w==",
                SecurityStamp = "SEJEPXC646ZBNCDYSM3H5FRK5RWP2TN6",
                ConcurrencyStamp = "b09a83ae-cfd3-4ee7-97e6-fbcf0b0fe78c",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            List<IdentityUserRole<int>> adminUserRoles = new List<IdentityUserRole<int>>()
            {
                new IdentityUserRole<int>()
                {
                    UserId = 1,
                    RoleId = 1
                },
                new IdentityUserRole<int>()
                {
                    UserId = 1,
                    RoleId = 2
                },
                new IdentityUserRole<int>()
                {
                    UserId = 1,
                    RoleId = 3
                }
            };

            return (admin, adminUserRoles);
        }


        public (User, List<IdentityUserRole<int>>) CreateManagerWithRoles()
        {
            User manager = new User()
            {
                Id = 2,
                FirstName = "Managerek",
                LastName = "Managerovy",
                UserName = "manager",
                NormalizedUserName = "MANAGER",
                Email = "manager@manager.cz",
                NormalizedEmail = "MANAGER@MANAGER.CZ",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==",
                SecurityStamp = "MAJXOSATJKOEM4YFF32Y5G2XPR5OFEL6",
                ConcurrencyStamp = "7a8d96fd-5918-441b-b800-cbafa99de97b",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            List<IdentityUserRole<int>> managerUserRoles = new List<IdentityUserRole<int>>()
            {
                new IdentityUserRole<int>()
                {
                    UserId = 2,
                    RoleId = 2
                },
                new IdentityUserRole<int>()
                {
                    UserId = 2,
                    RoleId = 3
                }
            };

            return (manager, managerUserRoles);
        }
    }
}
