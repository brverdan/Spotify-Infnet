using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Spotify.API.Controllers;
using Spotify.Application.Interfaces;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Domain.Accounts.ValueObjects;

namespace Spotify.UnitTests.Controller;

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

        var instancia = new UserController(userService.Object);
        
        // Act
        var result = instancia.GetUserById(Guid.NewGuid());

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

        var instancia = new UserController(userService.Object);

        // Act
        var result = instancia.GetUserById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}