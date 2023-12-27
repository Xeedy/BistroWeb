using System;
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
        public List<Brewery> GetBrewery()
        {
            List<Brewery> breweries = new List<Brewery>();

            breweries.Add(new Brewery
            {
                Id = 1,
                Name = "Testovci pivovar",
                Description = "Testovací sada",
                ImageSrc = "/img/products/produkty-01.jpg"
            });
            breweries.Add(new Brewery
            {
                Id = 2,
                Name = "Testingy",
                Description = "Sth",
                ImageSrc = "/img/products/produkty-01.jpg"
            });

            return breweries;
        }
        public IList<Product> GetProducts(IList<Brewery> breweries)
        {
            IList<Product> products = new List<Product>();
            if (breweries.Count >= 2)
            {
                products.Add(new Product
                {
                    Id = 1,
                    Name = "Test",
                    BreweryId = breweries[0].Id,  // Use the actual Id of the first Brewery
                    Description = "Testovací sada",
                    Price = 999,
                    ImageSrc = "/img/products/produkty-01.jpg"
                });

                products.Add(new Product
                {
                    Id = 2,
                    Name = "Testovacka",
                    BreweryId = breweries[1].Id,  // Use the actual Id of the second Brewery
                    Description = "Cosik",
                    Price = 10,
                    ImageSrc = "/img/products/produkty-01.jpg"
                });

                return products;
            }
            else
            {
                throw new InvalidOperationException("Not enough breweries in the list to initialize products.");
            }
        }



        public IList<Item> GetItems()
        {
            IList<Item> items = new List<Item>();

            items.Add(new Item
            {
                Id = 1,
                Name = "Kofola (0,3 l/0,5 l)",
                Description = "",
                Price = 35,
                Section = "Nápoje",
                Price2 = 25
            }); ;
            items.Add(new Item
            {
                Id = 2,
                Name = "Royal Crown Cola (0,3 l/0,5 l)",
                Description = "",
                Price = 40,
                Section = "Nápoje",
                Price2 = 30
            }); ;
            items.Add(new Item
            {
                Id = 3,
                Name = "River Tonic (0,5 l)",
                Description = "",
                Price = 35,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 4,
                Name = "Džus Rauch",
                Description = "Jahoda, Hruška",
                Price = 45,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 5,
                Name = "Džus Rani",
                Description = "Broskev, Mango, Pomeranč, Ananas",
                Price = 45,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 6,
                Name = "Mošt jablečný (0,3 l/0,5 l)",
                Description = "",
                Price = 50,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 7,
                Name = "Rajec neperlivý, jemně perlivý (0,33 l)",
                Description = "",
                Price = 35,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 8,
                Name = "Kohoutková voda s citronem (1 l)",
                Description = "",
                Price = 39,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 9,
                Name = "Domácí limonáda",
                Description = "Máta, Bez, Zázvor, Malina, Višeň, Grapefruit, Pomeranč",
                Price = 49,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 10,
                Name = "Mléčný koktejl",
                Description = "Pistácie, Banán, Vanilka",
                Price = 55,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 11,
                Name = "Svařené víno",
                Description = "Červené, Bílé",
                Price = 59,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 12,
                Name = "Espresso",
                Description = "",
                Price = 50,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 13,
                Name = "Cappuccino",
                Description = "",
                Price = 60,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 14,
                Name = "Starshollowské kakao (0,4 l)",
                Description = "",
                Price = 59,
                Section = "Nápoje"
            }); ;
            items.Add(new Item
            {
                Id = 15,
                Name = "Čepované pivo (4 pípy - dle aktuální nabídky)",
                Description = "",
                Price = 0,
                Section = "Pivo"
            }); ;
            items.Add(new Item
            {
                Id = 16,
                Name = "Láhvová piva  (cca 200 druhů - dle aktuální nabídky)",
                Description = "",
                Price = 0,
                Section = "Pivo"
            }); ;
            items.Add(new Item
            {
                Id = 17,
                Name = "Nealko pivo Bernard Jantar polotmavý (láhev 0,5 l)",
                Description = "",
                Price = 40,
                Section = "Pivo"
            }); ;
            items.Add(new Item
            {
                Id = 18,
                Name = "Nealko Bernard Švestka (láhev 0,5 l)",
                Description = "",
                Price = 40,
                Section = "Pivo"
            }); ;
            items.Add(new Item
            {
                Id = 19,
                Name = "Strongbow Apple Cider (plech 0,4 l)",
                Description = "",
                Price = 40,
                Section = "Pivo"
            }); ;
            items.Add(new Item
            {
                Id = 20,
                Name = "Bíle víno San Zeno (0,2 l)",
                Description = "",
                Price = 44,
                Section = "Vína"
            }); ;
            items.Add(new Item
            {
                Id = 21,
                Name = "Červené víno Turano (0,2 l)",
                Description = "",
                Price = 44,
                Section = "Vína",
                Price2 = 0
            }); ;
            items.Add(new Item
            {
                Id = 22,
                Name = "Cuba Libre (0,3 l)",
                Description = "Captain Morgan s Colou",
                Price = 69,
                Section = "Míchané"
            }); ;
            items.Add(new Item
            {
                Id = 23,
                Name = "Gin Tonic (0,3 l)",
                Description = "",
                Price = 75,
                Section = "Míchané"
            }); ;
            items.Add(new Item
            {
                Id = 24,
                Name = "Jack Daniel's Tenessee Whisky (0,04 l)",
                Description = "Klasik, Honey, Fire",
                Price = 72,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 25,
                Name = "Johny Walker Red Label (0,04 l)",
                Description = "",
                Price = 65,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 26,
                Name = "Tullamore Dew (0,04 l)",
                Description = "",
                Price = 65,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 27,
                Name = "Jameson",
                Description = "",
                Price = 65,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 28,
                Name = "Vodka Russian Standart (0,04 l)",
                Description = "",
                Price = 45,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 29,
                Name = "Vodka Finlandia (0,04 l)",
                Description = "",
                Price = 45,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 30,
                Name = "Slivovice Budík Jelínek (0,04 l)",
                Description = "",
                Price = 60,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 31,
                Name = "Meruňkovice (0,04 l)",
                Description = "",
                Price = 60,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 32,
                Name = "Spišská borovička (0,04 l)",
                Description = "",
                Price = 45,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 33,
                Name = "Becherovka (0,04 l)",
                Description = "",
                Price = 40,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 34,
                Name = " Jägermeister (0,04 l)",
                Description = "",
                Price = 50,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 35,
                Name = "Lysá Hora bylinný likér (0,04 l)",
                Description = "",
                Price = 45,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 36,
                Name = "Metaxa 5* (0,04 l)",
                Description = "",
                Price = 55,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 37,
                Name = "Beefeater Gin (0,04 l)",
                Description = "",
                Price = 50,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 38,
                Name = "Rum Ron Zacapa Centenario 23YO (0,04 l)",
                Description = "",
                Price = 115,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 39,
                Name = "Rum Diplomatico Reserva Exclusiva 12YO (0,04 l)",
                Description = "",
                Price = 90,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 40,
                Name = "Rum Legendario Elixír De Cuba (0,04 l)",
                Description = "",
                Price = 75,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 41,
                Name = "Rum Ron Pampero Especial (0,04 l)",
                Description = "",
                Price = 70,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 42,
                Name = "Heffron Rum (0,04 l)",
                Description = "",
                Price = 55,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 43,
                Name = "Rum Captain Morgan Original Spiced Gold (0,04 l)",
                Description = "",
                Price = 49,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 44,
                Name = "Rum Havana Club Aňejo 3 Aňos (0,04 l)",
                Description = "",
                Price = 50,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 45,
                Name = "Fernet Stock (0,04 l)",
                Description = "Original, Citrus",
                Price = 39,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 46,
                Name = "Griotka (0,04 l)",
                Description = "",
                Price = 35,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 47,
                Name = "Peprmintový likér (0,04 l)",
                Description = "",
                Price = 30,
                Section = "Destiláty"
            }); ;
            items.Add(new Item
            {
                Id = 48,
                Name = "1. Palačinka se špenátem, sýrem a kukuřicí",
                Description = "Niva, Gouda, Balkánský sýr, Lahůdkové tofu",
                Price = 129,
                Section = "Palačinky"
            }); ;
            items.Add(new Item
            {
                Id = 49,
                Name = "2. Palačinka s tomatovým dipem, sýrem, kukuřicí a olivami",
                Description = "Niva, Gouda, Balkánský sýr, Lahůdkové tofu",
                Price = 129,
                Section = "Palačinky"
            }); ;
            items.Add(new Item
            {
                Id = 50,
                Name = "3. Palačinka s domácím chutney, sýrem, bazalkovým kořením a balsamicem",
                Description = "Niva, Gouda, Balkánský sýr, Lahůdkové tofu",
                Price = 129,
                Section = "Palačinky"
            }); ;
            items.Add(new Item
            {
                Id = 51,
                Name = "4. Sendvič s rajčatovou omáčkou, lahůdkovým tofu, kyselou okurkou a karamelizovanou cibulkou (*V)",
                Description = "",
                Price = 99,
                Section = "Sendviče"
            }); ;
            items.Add(new Item
            {
                Id = 52,
                Name = "5. Sendvič s rajčatovou omáčkou, schwarzvaldskou šunkou, parmesanem a rajčetem",
                Description = "",
                Price = 99,
                Section = "Sendviče"
            }); ;
            items.Add(new Item
            {
                Id = 53,
                Name = "6. Sendvič s rajčatovou omáčkou, schwarzvaldskou šunkou, nivou a kukuřicí",
                Description = "",
                Price = 99,
                Section = "Sendviče"
            }); ;
            items.Add(new Item
            {
                Id = 54,
                Name = "Pita Libanon - Pita s olivovým olejem a kořením zátar (*V)",
                Description = "",
                Price = 55,
                Section = "Pity"
            }); ;
            items.Add(new Item
            {
                Id = 55,
                Name = "Pita Balkán - Balkánský sýr, rajče, kardamom",
                Description = "",
                Price = 75,
                Section = "Pity"
            }); ;
            items.Add(new Item
            {
                Id = 56,
                Name = "Pita Tomato - Balkánský sýr, rajče, bazalka",
                Description = "",
                Price = 75,
                Section = "Pity"
            }); ;
            items.Add(new Item
            {
                Id = 57,
                Name = "Pita Kuku - Rajčatová omáčka, niva, kukuřice",
                Description = "",
                Price = 55,
                Section = "Pity"
            }); ;
            items.Add(new Item
            {
                Id = 58,
                Name = "Domácí Hummus (130g) s pitou (2ks), zakápnutý olivovým olejem (*V)",
                Description = "",
                Price = 95,
                Section = "Speciality"
            }); ;
            items.Add(new Item
            {
                Id = 59,
                Name = "Domácí slaný popcorn (*V)",
                Description = "",
                Price = 45,
                Section = "Speciality"
            }); ;
            items.Add(new Item
            {
                Id = 60,
                Name = "Homestyle hranolky (200g) (*V) s dipem",
                Description = "Česnekový (*V), Tatarka, Kečup, Sweetchilli",
                Price = 59,
                Section = "Speciality"
            }); ;
            items.Add(new Item
            {
                Id = 61,
                Name = "Palačinky s marmeládou, zmrzlinou a šlehačkou",
                Description = "",
                Price = 99,
                Section = "Dezerty"
            }); ;
            items.Add(new Item
            {
                Id = 62,
                Name = "Vanilková zmrzlina (1 kopeček)",
                Description = "",
                Price = 23,
                Section = "Dezerty"
            }); ;
            items.Add(new Item
            {
                Id = 63,
                Name = "Kešu natural (50g)",
                Description = "",
                Price = 49,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 64,
                Name = "Brambůrky (100g)",
                Description = "Solené, Hořčicové",
                Price = 40,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 65,
                Name = "Kroužky Jarní cibulka (50g)",
                Description = "",
                Price = 35,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 66,
                Name = "Tyčinky Bertyčky - různé druhy (90g)",
                Description = "",
                Price = 30,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 67,
                Name = "Křupky (60g)",
                Description = "",
                Price = 25,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 68,
                Name = "Perníčky v čokoládě (70g)",
                Description = "",
                Price = 35,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 69,
                Name = "Wasabi (50g)",
                Description = "",
                Price = 35,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 70,
                Name = "Japonská směs (50g)",
                Description = "",
                Price = 35,
                Section = "Doplňky"
            }); ;
            items.Add(new Item
            {
                Id = 71,
                Name = "Zelený čaj s marockou mátou Tuarég (konvička cca 4dcl)",
                Description = "",
                Price = 80,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 72,
                Name = "Čínský zelený čaj Vzácné obočí (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 73,
                Name = "Čínský tmavý čaj Puerh (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 74,
                Name = "Cejlonský černý čaj Cejlon Adam's Peak (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 75,
                Name = "Ovocný čaj Indiánské léto (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 76,
                Name = "Ibiškový čaj faraónů Karkade (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 77,
                Name = "Bylinkový čaj Rooibos (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 78,
                Name = "Lipový čaj (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 79,
                Name = "Mateřídouškový čaj (konvička cca 4dcl)",
                Description = "",
                Price = 70,
                Section = "Čaje"
            }); ;
            items.Add(new Item
            {
                Id = 80,
                Name = "Zázvorový čaj s medem (konvička cca 4dcl)",
                Description = "",
                Price = 80,
                Section = "Čaje"
            }); ;


            return items;
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
