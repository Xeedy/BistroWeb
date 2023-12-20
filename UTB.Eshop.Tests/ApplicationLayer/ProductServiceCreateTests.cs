using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Moq;

using BistroWeb.Application.Abstraction;
using BistroWeb.Infrastructure.Database;
using BistroWeb.Domain.Entities;
using BistroWeb.Application.Implementation;

namespace BistroWeb.Tests.ApplicationLayer
{
    public class ProductServiceCreateTests
    {
        [Fact]
        public async Task Create_Success()
        {
            // Arrange

            //nastaveni falesne sluzby pro produkty
            var pathToImage = "img/product/UploadImageFile.png";
            var fileUpload = new Mock<IFileUploadService>();
            fileUpload.Setup(fu => fu.FileUploadAsync(It.IsAny<IFormFile>(), It.IsAny<string>())).Returns(() => Task.Run<string>(() => pathToImage));


            //nastaveni falesne IFormFile
            Mock<IFormFile> iffMock = new Mock<IFormFile>();
            iffMock.Setup(iff => iff.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None))
                                    .Callback<Stream, CancellationToken>((stream, token) =>
                                    {
                                        return;
                                    })
                                    .Returns(Task.CompletedTask);



            //Nainstalovan Nuget package: Microsoft.EntityFrameworkCore.InMemory
            //databazi vytvori v pameti
            DbContextOptions options = new DbContextOptionsBuilder<EshopDbContext>()
                                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                       .Options;
            var databaseContext = new EshopDbContext(options);
            databaseContext.Database.EnsureCreated();
            //smazani inicializacnich dat, pokud existuji
            databaseContext.Products.RemoveRange(databaseContext.Products);
            databaseContext.SaveChanges();



            ProductAppService service = new ProductAppService(fileUpload.Object, databaseContext);


            Product testProduct = GetTestProduct(iffMock.Object);



            //Act
            await service.Create(testProduct);



            // Assert
            Assert.Single(databaseContext.Products);

            Product addedProduct = databaseContext.Products.First();
            Assert.Equal(testProduct.Id, addedProduct.Id);
            Assert.NotNull(addedProduct.Name);
            Assert.Matches(testProduct.Name, addedProduct.Name);
            Assert.Equal(testProduct.Price, addedProduct.Price);
            Assert.NotNull(addedProduct.ImageSrc);
            Assert.Matches(pathToImage, addedProduct.ImageSrc);

        }



        Product GetTestProduct(IFormFile iff)
        {
            return new Product()
            {
                Id = 1,
                Name = "produkt",
                Price = 10,
                ImageSrc = String.Empty,
                Image = iff
            };
        }

    }
}
