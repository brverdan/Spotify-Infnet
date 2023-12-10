using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotify.API.Controllers;
using Spotify.Application.Interfaces;
using Spotify.Application.Users.Dtos;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Domain.Accounts.ValueObjects;

namespace Spotify.UnitTests.Api.Controllers;

[Trait(nameof(UserController), "")]
public class UserControllerTests
{
    [Fact]
    public void MustGetUserByIdSucess()
    {
        // Arrange
        var userExpected = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test name",
            Email = "Test email",
            Cpf = new CPF { Numero = "80549664076" },
            CreditCards = new List<CreditCard>(),
            Playlists = new List<Playlist>(),
            Subscriptions = new List<Subscription>(),
            Favorites = new List<Favorite>()
        };

        var userService = new Mock<IUserService>();

        userService.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(userExpected);

        var instance = new UserController(userService.Object);

        // Act
        var result = instance.GetUserById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        result.As<OkObjectResult>().Value.Should().NotBeNull();
        result.As<OkObjectResult>().Value.Should().BeEquivalentTo(userExpected);
    }

    [Fact]
    public void MustGetUserByIdNotFoundUser()
    {
        // Arrange
        var userService = new Mock<IUserService>();

        userService.Setup(x => x.GetUserById(It.IsAny<Guid>()));

        var instance = new UserController(userService.Object);

        // Act
        var result = instance.GetUserById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task MustCreateUserSucess()
    {
        // Arrange
        var userExpected = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test name",
            Email = "Test email",
            Cpf = new CPF { Numero = "80549664076" },
            CreditCards = new List<CreditCard>(),
            Playlists = new List<Playlist>(),
            Subscriptions = new List<Subscription>(),
            Favorites = new List<Favorite>()
        };

        var createUserDto = new CreateUserDto
        {
            Name = "Teste name",
            Email = "Teste email",
            Cpf = "80549664076",
            Plan = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            CreditCard = new CreditCardDto
            {
                Number = "123456789",
                ActiveCard = true,
                AvailableLimit = 1000,
                SecureCode = 123
            }
        };

        var userService = new Mock<IUserService>();

        userService.Setup(x => x.CreateUser(createUserDto)).ReturnsAsync(userExpected);

        var instance = new UserController(userService.Object);

        // Act
        var result = await instance.CreateUser(createUserDto);

        // Assert
        result.Should().BeOfType<CreatedResult>();

        result.As<CreatedResult>().Value.Should().NotBeNull();
        result.As<CreatedResult>().Value.Should().BeEquivalentTo(userExpected);
    }

    [Fact]
    public async Task MustCreateUserFail()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            Name = null,
            Email = string.Empty,
            Cpf = string.Empty,
            Plan = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94")
        };

        var userService = new Mock<IUserService>();


        var instance = new UserController(userService.Object);

        instance.ModelState.AddModelError("", "Invalid dto");

        // Act
        var result = await instance.CreateUser(createUserDto);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
}