using FluentAssertions;
using Moq;
using Spotify.Application.Users;
using Spotify.Application.Users.Dtos;
using Spotify.Core.Exceptions;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Domain.Accounts.ValueObjects;
using Spotify.Domain.Streaming.Aggregates;
using Spotify.Infrastructure.Interfaces;

namespace Spotify.UnitTests.Application.Services;

[Trait(nameof(UserService), "")]
public class UserServiceTests
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

        var bandRepository = new Mock<IBandRepository>();
        var planRepository = new Mock<IPlanRepository>();
        var userRepository = new Mock<IUserRepository>();

        userRepository.Setup(x => x.GetUserById(It.IsAny<Guid>())).Returns(userExpected);

        var instance = new UserService(userRepository.Object,
                                        planRepository.Object,
                                        bandRepository.Object);

        // Act
        var result = instance.GetUserById(Guid.NewGuid());

        // Assert
        result.Should().BeOfType<User>();
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(userExpected);
    }

    [Fact]
    public void MustGetUserByIdFail_UserNotFound()
    {
        // Arrange
        var bandRepository = new Mock<IBandRepository>();
        var planRepository = new Mock<IPlanRepository>();
        var userRepository = new Mock<IUserRepository>();

        userRepository.Setup(x => x.GetUserById(It.IsAny<Guid>()));

        var instance = new UserService(userRepository.Object,
                                        planRepository.Object,
                                        bandRepository.Object);

        // Act
        var result = () => instance.GetUserById(Guid.NewGuid());

        // Assert
        result.Should().Throw<BusinessException>();
    }

    [Fact]
    public async Task MustCreateUserSuccess()
    {
        // Arrange
        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
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

        var bandRepository = new Mock<IBandRepository>();
        var planRepository = new Mock<IPlanRepository>();
        var userRepository = new Mock<IUserRepository>();

        planRepository.Setup(x => x.GetPlanById(It.IsAny<Guid>())).ReturnsAsync(plan);

        userRepository.Setup(x => x.CreateUser(It.IsAny<User>()));


        var instance = new UserService(userRepository.Object,
                                        planRepository.Object,
                                        bandRepository.Object);

        // Act
        var result = await instance.CreateUser(createUserDto);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
    }

    [Fact]
    public void MustCreateUserFail_PlanNotFound()
    {
        // Arrange
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

        var bandRepository = new Mock<IBandRepository>();
        var planRepository = new Mock<IPlanRepository>();
        var userRepository = new Mock<IUserRepository>();

        userRepository.Setup(x => x.CreateUser(It.IsAny<User>()));


        var instance = new UserService(userRepository.Object,
                                        planRepository.Object,
                                        bandRepository.Object);

        // Act
        var result = async () => await instance.CreateUser(createUserDto);

        // Assert
        result.Should().ThrowAsync<BusinessException>();
    }
}