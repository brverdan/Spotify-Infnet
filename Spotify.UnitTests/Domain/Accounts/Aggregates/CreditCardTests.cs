using FluentAssertions;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Domain.Accounts.Exceptions;
using Spotify.Domain.Streaming.Aggregates;
using Spotify.Domain.Transactions.Aggregates;

namespace Spotify.UnitTests.Domain.Accounts.Aggregates;

[Trait(nameof(CreditCard), "")]
public class CreditCardTests
{
    [Fact]
    public void MustCreateCreditCardSucess()
    {
        // Arrange
        string number = "5367408000346032";
        string username = "Teste name";
        double availableLimit = 10000;
        bool activeCard = true;
        int secureCode = 123;

        var creditCard = new CreditCard();

        // Act
        creditCard.CreateCard(number, availableLimit, activeCard, secureCode, username);

        // Assert
        creditCard.Id.Should().NotBeEmpty();
        creditCard.Number.Should().NotBeNullOrWhiteSpace().And.Be(number);
        creditCard.AvailableLimit.Should().NotBe(0).And.Be(availableLimit);
        creditCard.ActiveCard.Should().BeTrue();
        creditCard.SecureCode.Should().NotBe(0).And.Be(secureCode);
        creditCard.Username.Should().NotBeNullOrWhiteSpace().And.Be(username);
    }

    [Fact]
    public void MustCreateTransactionSucess()
    {
        // Arrange
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

        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        // Act
        creditCard.CreateTransaction(plan.Name, plan.Value);

        // Assert
        creditCard.Transactions.Should().NotBeNullOrEmpty().And.HaveCount(1);
    }

    [Fact]
    public void MustCreateTransactionFail_CardIsNotActive()
    {
        // Arrange
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

        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        // Act
        var result = () => creditCard.CreateTransaction(plan.Name, plan.Value);

        // Assert
        result.Should().Throw<CardException>().And.Errors[0].ErrorMessage.Should().Contain("Card is not active");
    }

    [Fact]
    public void MustCreateTransactionFail_CardHasNoLimit()
    {
        // Arrange
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

        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        // Act
        var result = () => creditCard.CreateTransaction(plan.Name, plan.Value);

        // Assert
        result.Should().Throw<CardException>().And.Errors[0].ErrorMessage.Should().Contain("Not limit available");
    }

    [Fact]
    public void MustCreateTransactionFail_TransactionInvalid_TooManyTransactions()
    {
        // Arrange
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

        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        var transactions = new List<Transaction>
        {
            new Transaction 
            {
                Description = "Description 1",
                PurchasedDate = DateTime.Now,
                Amount = 100
            },
            new Transaction
            {
                Description = "Description 2",
                PurchasedDate = DateTime.Now,
                Amount = 200
            },
            new Transaction
            {
                Description = "Description 3",
                PurchasedDate = DateTime.Now,
                Amount = 400
            },
            new Transaction
            {
                Description = "Description 4",
                PurchasedDate = DateTime.Now,
                Amount = 500
            }
        };

        creditCard.Transactions.AddRange(transactions);

        // Act
        var result = () => creditCard.CreateTransaction(plan.Name, plan.Value);

        // Assert
        result.Should().Throw<CardException>()
            .And.Errors[0]
            .ErrorMessage.Should().Contain("Too many transactions have been made, please wait a moment and try again");
    }

    [Fact]
    public void MustCreateTransactionFail_TransactionInvalid_DuplicateTransaction()
    {
        // Arrange
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

        var plan = new Plan
        {
            Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
            Name = "Test plan",
            Description = "Description",
            Value = 100
        };

        var transactions = new List<Transaction>
        {
            new Transaction
            {
                Description = "Test plan purchase",
                PurchasedDate = DateTime.Now,
                Amount = 100
            }
        };

        creditCard.Transactions.AddRange(transactions);

        // Act
        var result = () => creditCard.CreateTransaction(plan.Name, plan.Value);

        // Assert
        result.Should().Throw<CardException>()
            .And.Errors[0]
            .ErrorMessage.Should().Contain("Duplicate transaction");
    }
}