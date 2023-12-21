using System;
using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;
using BistroWeb.Application.Abstraction;
using BistroWeb.Domain.Entities;
using BistroWeb.Infrastructure.Database;
using BistroWeb.Web.Areas.Admin.Controllers;

namespace BistroWeb.Tests.Admin.MenuItemController
{
    public class MenuItemControllerCreateTests
    {
        [Fact]
        public async Task Create_success()
        {
            // Arrange
            DatabaseFake.Items.Clear();

            Mock<IMenuItemAppService> itemServiceMock = new Mock<IMenuItemAppService>();
            itemServiceMock.Setup(itemService => itemService.Create(It.IsAny<Item>()))
                .Returns<Item>(item => Task.Run(() => { DatabaseFake.Items.Add(item); }));

            // Mock IFileUploadService
            Mock<IFileUploadService> fileUploadServiceMock = new Mock<IFileUploadService>();

            // Mock EshopDbContext
            Mock<EshopDbContext> eshopDbContextMock = new Mock<EshopDbContext>();

            var item = GetItem();

            // Pass the mocked EshopDbContext to the MenuItemController constructor
            var itemController = new Web.Areas.Admin.Controllers.MenuItemController(itemServiceMock.Object, eshopDbContextMock.Object, fileUploadServiceMock.Object);

            // Act
            var actionResult = await itemController.Create(item);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.NotNull(redirectToActionResult.ActionName);
            Assert.Equal(nameof(Web.Areas.Admin.Controllers.MenuItemController.Index), redirectToActionResult.ActionName);

            Assert.NotEmpty(DatabaseFake.Items);
            Assert.Single(DatabaseFake.Items);
        }

        Item GetItem()
        {
            return new Item()
            {
                Id = 120,
                Name = "Item",
                Price = 1.0,
                Description = String.Empty,
                Section = "Ahoj",
                Price2 = 1.0
            };
        }
    }
}

