using FluentAssertions;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Domain.Accounts.Exceptions;
using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.UnitTests.Domain.Accounts.Aggregates;

[Trait(nameof(User), "")]
public class UserTests
{
    [Fact]
    public void MustCreateUserSucess()
    {
        // Arrange
        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        var creditCard = new CreditCard
        {
            Id = Guid.NewGuid(),
            Number = "5367408000346032",
            Username = "Teste name",
            AvailableLimit = 10000,
            ValidDate = DateTime.Now,
            ActiveCard = true,
            SecureCode = 123         
        };

        string name = "Teste name";
        string email = "Test email";
        string cpf = "80549664076";

        var user = new User();

        // Act
        user.Create(name, email, cpf, creditCard, plan);

        // Assert
        user.Cpf.Should().NotBeNull();
        user.Name.Should().NotBeNullOrWhiteSpace();
        user.Email.Should().NotBeNullOrWhiteSpace();
        user.CreditCards.Should().NotBeNullOrEmpty();
        user.Subscriptions.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void MustCreateUser_CPFInvalid()
    {
        // Arrange
        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        var creditCard = new CreditCard
        {
            Id = Guid.NewGuid(),
            Number = "5367408000346032",
            Username = "Teste name",
            AvailableLimit = 10000,
            ValidDate = DateTime.Now,
            ActiveCard = true,
            SecureCode = 123
        };

        string name = "Teste name";
        string email = "Test email";
        string cpf = "12345678900";

        var user = new User();

        // Act
        var result = () => user.Create(name, email, cpf, creditCard, plan);

        // Assert
        result.Should().Throw<CPFException>();
    }

    [Fact]
    public void MustCreateUserFail_CreditCardNotActive()
    {
        // Arrange
        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        var creditCard = new CreditCard
        {
            Id = Guid.NewGuid(),
            Number = "5367408000346032",
            Username = "Teste name",
            AvailableLimit = 10000,
            ValidDate = DateTime.Now,
            ActiveCard = false,
            SecureCode = 123
        };

        string name = "Teste name";
        string email = "Test email";
        string cpf = "80549664076";

        var user = new User();

        // Act
        var result = () => user.Create(name, email, cpf, creditCard, plan);

        // Assert
        result.Should().Throw<CardException>().And.Errors[0].ErrorMessage.Should().Contain("Card is not active");
    }

    [Fact]
    public void MustCreateUserFail_CreditCardHasNoLimit()
    {
        // Arrange
        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        var creditCard = new CreditCard
        {
            Id = Guid.NewGuid(),
            Number = "5367408000346032",
            Username = "Teste name",
            AvailableLimit = 0,
            ValidDate = DateTime.Now,
            ActiveCard = true,
            SecureCode = 123
        };

        string name = "Teste name";
        string email = "Test email";
        string cpf = "80549664076";

        var user = new User();

        // Act
        var result = () => user.Create(name, email, cpf, creditCard, plan);

        // Assert
        result.Should().Throw<CardException>().And.Errors[0].ErrorMessage.Should().Contain("Not limit available");
    }
}