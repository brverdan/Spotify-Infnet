using FluentAssertions;
using FluentAssertions.Extensions;
using Spotify.Domain.Transactions.Aggregates;

namespace Spotify.UnitTests.Domain.Transactions.Aggregates;

[Trait(nameof(Transaction), "")]
public class TransactionTests
{
    [Fact]
    public void MustCreateCreditCardSucess()
    {
        // Arrange
        string username = "Teste name";
        double amount = 10000;
        string planName = "Test plan";

        var transaction = new Transaction();

        // Act
        transaction.Create(username, amount, planName);

        // Assert
        transaction.Username.Should().NotBeNullOrWhiteSpace().And.Be(username);
        transaction.Amount.Should().NotBe(0).And.Be(amount);
        transaction.Merchant.Should().NotBeNull();
        transaction.Merchant.MerchantName.Should().NotBeNullOrWhiteSpace().And.Be("Spotify");
        transaction.PurchasedDate.Should().BeCloseTo(DateTime.Now, 2.Seconds());
        transaction.Description.Should().NotBeNullOrWhiteSpace().And.Be($"{planName} purchase");
    }
}