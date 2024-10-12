using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using BistroWeb.Application.Implementation;
using BistroWeb.Application.ViewModels;
using BistroWeb.Infrastructure.Identity.Enums;
using BistroWeb.Infrastructure.Identity;

public class AccountIdentityServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<SignInManager<User>> _signInManagerMock;
    private readonly AccountIdentityService _accountService;

    public AccountIdentityServiceTests()
    {
        _userManagerMock = new Mock<UserManager<User>>(/* ... initialize mock ... */);
        _signInManagerMock = new Mock<SignInManager<User>>(/* ... initialize mock ... */);
        _accountService = new AccountIdentityService(_userManagerMock.Object, _signInManagerMock.Object);
    }

    [Fact]
    public async Task Register_ShouldReturnErrors_WhenRegistrationFails()
    {
        // Arrange
        var registerVm = new RegisterViewModel
        {
            Username = "testuser",
            Password = "Password123",
            RepeatedPassword = "Password123",
            Email = "test@example.com"
        };

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

        // Act
        var result = await _accountService.Register(registerVm, Roles.Customer);

        // Assert
        Assert.Single(result);
        Assert.Equal("Error", result[0]);
    }

    [Fact]
    public async Task Register_ShouldCreateUser_WhenRegistrationSucceeds()
    {
        // Arrange
        var registerVm = new RegisterViewModel
        {
            Username = "testuser",
            Password = "Password123",
            RepeatedPassword = "Password123",
            Email = "test@example.com"
        };

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _accountService.Register(registerVm, Roles.Customer);

        // Assert
        Assert.Null(result);
    }
}
