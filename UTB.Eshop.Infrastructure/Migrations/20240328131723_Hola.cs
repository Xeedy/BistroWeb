using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BistroWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Hola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Breweries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageSrc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breweries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Carousels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImageSrc = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageAlt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carousels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(110)", maxLength: 110, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Section = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price2 = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Missings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(110)", maxLength: 110, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Typees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typees", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shifts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(90)", maxLength: 90, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false),
                    ImageSrc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    New = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BreweryId = table.Column<int>(type: "int", nullable: false),
                    TypeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Typees_TypeeId",
                        column: x => x.TypeeId,
                        principalTable: "Typees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tappeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(90)", maxLength: 90, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MainPrice = table.Column<double>(type: "double", nullable: false),
                    OtherPrice = table.Column<double>(type: "double", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BreweryId = table.Column<int>(type: "int", nullable: false),
                    TypeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tappeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tappeds_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalTable: "Breweries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tappeds_Typees_TypeeId",
                        column: x => x.TypeeId,
                        principalTable: "Typees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RatingValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "9cf14c2c-19e7-40d6-b744-8917505c3106", "Admin", "ADMIN" },
                    { 2, "be0efcde-9d0a-461d-8eb6-444b043d6660", "Manager", "MANAGER" },
                    { 3, "29dafca7-cd20-4cd9-a3dd-4779d7bac3ee", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "b09a83ae-cfd3-4ee7-97e6-fbcf0b0fe78c", "michal.tesik1@gmail.com", true, "Michal", "Těšík", true, null, "MICHAL.TESIK1@GMAIL.COM", "XEEDY", "AQAAAAEAACcQAAAAEM9O98Suoh2o2JOK1ZOJScgOfQ21odn/k6EYUpGWnrbevCaBFFXrNL7JZxHNczhh/w==", null, false, "SEJEPXC646ZBNCDYSM3H5FRK5RWP2TN6", false, "Xeedy" },
                    { 2, 0, "7a8d96fd-5918-441b-b800-cbafa99de97b", "manager@manager.cz", true, "Managerek", "Managerovy", true, null, "MANAGER@MANAGER.CZ", "MANAGER", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", null, false, "MAJXOSATJKOEM4YFF32Y5G2XPR5OFEL6", false, "manager" }
                });

            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "Id", "Description", "ImageSrc", "Name" },
                values: new object[,]
                {
                    { 1, "Only for developement", "/img/brewery/Developement.jpg", "Testovci pivovar" },
                    { 2, "Sth", "/img/brewery/Belgium.png", "Belgická piva" },
                    { 3, "Sth", "/img/brewery/Beskyd.png", "Beskydský pivovárek" },
                    { 4, "Sth", "/img/brewery/CernyPotoka.jpg", "Černý potoka" },
                    { 5, "Sth", "/img/brewery/Cestmir.jpg", "Cestmir" },
                    { 6, "Sth", "/img/brewery/Clock.jpg", "Clock" },
                    { 7, "Sth", "/img/brewery/Cobolis.jpg", "Cobolis" },
                    { 8, "Sth", "/img/brewery/Dejf.jpg", "Dejf" },
                    { 9, "Sth", "/img/brewery/Haksna.png", "Haksna" },
                    { 10, "Sth", "/img/brewery/Holland.png", "Holandská piva" },
                    { 11, "Sth", "/img/brewery/Chroust.jpeg", "Chroust" },
                    { 12, "Sth", "/img/brewery/Kamenice.png", "Kamenice" },
                    { 13, "Sth", "/img/brewery/Konicek.png", "Koníček" },
                    { 14, "Sth", "/img/brewery/Matuska.png", "Matuška" },
                    { 15, "Sth", "/img/brewery/Mazak.png", "Mazák" },
                    { 16, "Sth", "/img/brewery/NachmelenaOpice.jpg", "Nachmelená opice" },
                    { 17, "Sth", "/img/brewery/Ogar.jpg", "Ogar" },
                    { 18, "Sth", "/img/brewery/Raven.png", "Raven" },
                    { 19, "Sth", "/img/brewery/Rotor.jpg", "Rotor" },
                    { 20, "Sth", "/img/brewery/Sibeeria.png", "Sibeeria" },
                    { 21, "Sth", "/img/brewery/USAUK.jpg", "Americká / Anglická piva" },
                    { 22, "Sth", "/img/brewery/Valasek.jpg", "Valášek" },
                    { 23, "Sth", "/img/brewery/Volt.jpg", "Volt" },
                    { 24, "Sth", "/img/brewery/wildcock.png", "WildCock" },
                    { 25, "Sth", "/img/brewery/WildCreatures.jpg", "Wild Creatures" },
                    { 26, "Sth", "/img/brewery/Zichovec.png", "Zichovec" }
                });

            migrationBuilder.InsertData(
                table: "Calendars",
                columns: new[] { "Id", "Description", "EndDate", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "Description of Sample Calendar 1", new DateTime(2024, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Calendar 1" },
                    { 2, "Description of Sample Calendar 2", new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample Calendar 2" }
                });

            migrationBuilder.InsertData(
                table: "Carousels",
                columns: new[] { "Id", "ImageAlt", "ImageSrc" },
                values: new object[,]
                {
                    { 1, "First slide", "/img/carousel/Bistro_1.jpg" },
                    { 2, "Second slide", "/img/carousel/Bistro_2.jpg" },
                    { 3, "Third slide", "/img/carousel/Bistro_3.jpg" },
                    { 4, "Third slide", "/img/carousel/Bistro_4.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Name", "Price", "Price2", "Section" },
                values: new object[,]
                {
                    { 1, "", "Kofola (0,3 l/0,5 l)", 35.0, 25.0, "Nápoje" },
                    { 2, "", "Royal Crown Cola (0,3 l/0,5 l)", 40.0, 30.0, "Nápoje" },
                    { 3, "", "River Tonic (0,5 l)", 35.0, null, "Nápoje" },
                    { 4, "Jahoda, Hruška", "Džus Rauch", 45.0, null, "Nápoje" },
                    { 5, "Broskev, Mango, Pomeranč, Ananas", "Džus Rani", 45.0, null, "Nápoje" },
                    { 6, "", "Mošt jablečný (0,3 l/0,5 l)", 50.0, null, "Nápoje" },
                    { 7, "", "Rajec neperlivý, jemně perlivý (0,33 l)", 35.0, null, "Nápoje" },
                    { 8, "", "Kohoutková voda s citronem (1 l)", 39.0, null, "Nápoje" },
                    { 9, "Máta, Bez, Zázvor, Malina, Višeň, Grapefruit, Pomeranč", "Domácí limonáda", 49.0, null, "Nápoje" },
                    { 10, "Pistácie, Banán, Vanilka", "Mléčný koktejl", 55.0, null, "Nápoje" },
                    { 11, "Červené, Bílé", "Svařené víno", 59.0, null, "Nápoje" },
                    { 12, "", "Espresso", 50.0, null, "Nápoje" },
                    { 13, "", "Cappuccino", 60.0, null, "Nápoje" },
                    { 14, "", "Starshollowské kakao (0,4 l)", 59.0, null, "Nápoje" },
                    { 15, "", "Čepované pivo (4 pípy - dle aktuální nabídky)", 0.0, null, "Pivo" },
                    { 16, "", "Láhvová piva  (cca 200 druhů - dle aktuální nabídky)", 0.0, null, "Pivo" },
                    { 17, "", "Nealko pivo Bernard Jantar polotmavý (láhev 0,5 l)", 40.0, null, "Pivo" },
                    { 18, "", "Nealko Bernard Švestka (láhev 0,5 l)", 40.0, null, "Pivo" },
                    { 19, "", "Strongbow Apple Cider (plech 0,4 l)", 40.0, null, "Pivo" },
                    { 20, "", "Bíle víno San Zeno (0,2 l)", 44.0, null, "Vína" },
                    { 21, "", "Červené víno Turano (0,2 l)", 44.0, 0.0, "Vína" },
                    { 22, "Captain Morgan s Colou", "Cuba Libre (0,3 l)", 69.0, null, "Míchané" },
                    { 23, "", "Gin Tonic (0,3 l)", 75.0, null, "Míchané" },
                    { 24, "Klasik, Honey, Fire", "Jack Daniel's Tenessee Whisky (0,04 l)", 72.0, null, "Destiláty" },
                    { 25, "", "Johny Walker Red Label (0,04 l)", 65.0, null, "Destiláty" },
                    { 26, "", "Tullamore Dew (0,04 l)", 65.0, null, "Destiláty" },
                    { 27, "", "Jameson", 65.0, null, "Destiláty" },
                    { 28, "", "Vodka Russian Standart (0,04 l)", 45.0, null, "Destiláty" },
                    { 29, "", "Vodka Finlandia (0,04 l)", 45.0, null, "Destiláty" },
                    { 30, "", "Slivovice Budík Jelínek (0,04 l)", 60.0, null, "Destiláty" },
                    { 31, "", "Meruňkovice (0,04 l)", 60.0, null, "Destiláty" },
                    { 32, "", "Spišská borovička (0,04 l)", 45.0, null, "Destiláty" },
                    { 33, "", "Becherovka (0,04 l)", 40.0, null, "Destiláty" },
                    { 34, "", " Jägermeister (0,04 l)", 50.0, null, "Destiláty" },
                    { 35, "", "Lysá Hora bylinný likér (0,04 l)", 45.0, null, "Destiláty" },
                    { 36, "", "Metaxa 5* (0,04 l)", 55.0, null, "Destiláty" },
                    { 37, "", "Beefeater Gin (0,04 l)", 50.0, null, "Destiláty" },
                    { 38, "", "Rum Ron Zacapa Centenario 23YO (0,04 l)", 115.0, null, "Destiláty" },
                    { 39, "", "Rum Diplomatico Reserva Exclusiva 12YO (0,04 l)", 90.0, null, "Destiláty" },
                    { 40, "", "Rum Legendario Elixír De Cuba (0,04 l)", 75.0, null, "Destiláty" },
                    { 41, "", "Rum Ron Pampero Especial (0,04 l)", 70.0, null, "Destiláty" },
                    { 42, "", "Heffron Rum (0,04 l)", 55.0, null, "Destiláty" },
                    { 43, "", "Rum Captain Morgan Original Spiced Gold (0,04 l)", 49.0, null, "Destiláty" },
                    { 44, "", "Rum Havana Club Aňejo 3 Aňos (0,04 l)", 50.0, null, "Destiláty" },
                    { 45, "Original, Citrus", "Fernet Stock (0,04 l)", 39.0, null, "Destiláty" },
                    { 46, "", "Griotka (0,04 l)", 35.0, null, "Destiláty" },
                    { 47, "", "Peprmintový likér (0,04 l)", 30.0, null, "Destiláty" },
                    { 48, "Niva, Gouda, Balkánský sýr, Lahůdkové tofu", "1. Palačinka se špenátem, sýrem a kukuřicí", 129.0, null, "Palačinky" },
                    { 49, "Niva, Gouda, Balkánský sýr, Lahůdkové tofu", "2. Palačinka s tomatovým dipem, sýrem, kukuřicí a olivami", 129.0, null, "Palačinky" },
                    { 50, "Niva, Gouda, Balkánský sýr, Lahůdkové tofu", "3. Palačinka s domácím chutney, sýrem, bazalkovým kořením a balsamicem", 129.0, null, "Palačinky" },
                    { 51, "", "4. Sendvič s rajčatovou omáčkou, lahůdkovým tofu, kyselou okurkou a karamelizovanou cibulkou (*V)", 99.0, null, "Sendviče" },
                    { 52, "", "5. Sendvič s rajčatovou omáčkou, schwarzvaldskou šunkou, parmesanem a rajčetem", 99.0, null, "Sendviče" },
                    { 53, "", "6. Sendvič s rajčatovou omáčkou, schwarzvaldskou šunkou, nivou a kukuřicí", 99.0, null, "Sendviče" },
                    { 54, "", "Pita Libanon - Pita s olivovým olejem a kořením zátar (*V)", 55.0, null, "Pity" },
                    { 55, "", "Pita Balkán - Balkánský sýr, rajče, kardamom", 75.0, null, "Pity" },
                    { 56, "", "Pita Tomato - Balkánský sýr, rajče, bazalka", 75.0, null, "Pity" },
                    { 57, "", "Pita Kuku - Rajčatová omáčka, niva, kukuřice", 55.0, null, "Pity" },
                    { 58, "", "Domácí Hummus (130g) s pitou (2ks), zakápnutý olivovým olejem (*V)", 95.0, null, "Speciality" },
                    { 59, "", "Domácí slaný popcorn (*V)", 45.0, null, "Speciality" },
                    { 60, "Česnekový (*V), Tatarka, Kečup, Sweetchilli", "Homestyle hranolky (200g) (*V) s dipem", 59.0, null, "Speciality" },
                    { 61, "", "Palačinky s marmeládou, zmrzlinou a šlehačkou", 99.0, null, "Dezerty" },
                    { 62, "", "Vanilková zmrzlina (1 kopeček)", 23.0, null, "Dezerty" },
                    { 63, "", "Kešu natural (50g)", 49.0, null, "Doplňky" },
                    { 64, "Solené, Hořčicové", "Brambůrky (100g)", 40.0, null, "Doplňky" },
                    { 65, "", "Kroužky Jarní cibulka (50g)", 35.0, null, "Doplňky" },
                    { 66, "", "Tyčinky Bertyčky - různé druhy (90g)", 30.0, null, "Doplňky" },
                    { 67, "", "Křupky (60g)", 25.0, null, "Doplňky" },
                    { 68, "", "Perníčky v čokoládě (70g)", 35.0, null, "Doplňky" },
                    { 69, "", "Wasabi (50g)", 35.0, null, "Doplňky" },
                    { 70, "", "Japonská směs (50g)", 35.0, null, "Doplňky" },
                    { 71, "", "Zelený čaj s marockou mátou Tuarég (konvička cca 4dcl)", 80.0, null, "Čaje" },
                    { 72, "", "Čínský zelený čaj Vzácné obočí (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 73, "", "Čínský tmavý čaj Puerh (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 74, "", "Cejlonský černý čaj Cejlon Adam's Peak (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 75, "", "Ovocný čaj Indiánské léto (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 76, "", "Ibiškový čaj faraónů Karkade (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 77, "", "Bylinkový čaj Rooibos (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 78, "", "Lipový čaj (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 79, "", "Mateřídouškový čaj (konvička cca 4dcl)", 70.0, null, "Čaje" },
                    { 80, "", "Zázvorový čaj s medem (konvička cca 4dcl)", 80.0, null, "Čaje" }
                });

            migrationBuilder.InsertData(
                table: "Missings",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Rajčata" },
                    { 2, "Poslední", "Cibule" }
                });

            migrationBuilder.InsertData(
                table: "Typees",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Only for developement", "IPA" },
                    { 2, "Only for developement", "APA" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 2, 2 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Active", "BreweryId", "Description", "ImageSrc", "Name", "New", "Price", "TypeeId" },
                values: new object[,]
                {
                    { 1, false, 1, "Testovací sada", "/img/products/produkty-01.jpg", "Test", false, 999.0, 1 },
                    { 2, false, 2, "Cosik", "/img/products/produkty-01.jpg", "Testovacka", false, 10.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tappeds",
                columns: new[] { "Id", "Active", "BreweryId", "Description", "MainPrice", "Name", "OtherPrice", "TypeeId" },
                values: new object[,]
                {
                    { 1, true, 1, "Testovací sada", 999.0, "Test", 0.0, 1 },
                    { 2, true, 2, "Cosik", 10.0, "Testovacka", 0.0, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BreweryId",
                table: "Products",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeeId",
                table: "Products",
                column: "TypeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductId",
                table: "Ratings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tappeds_BreweryId",
                table: "Tappeds",
                column: "BreweryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tappeds_TypeeId",
                table: "Tappeds",
                column: "TypeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "Carousels");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Missings");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Tappeds");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Breweries");

            migrationBuilder.DropTable(
                name: "Typees");
        }
    }
}
