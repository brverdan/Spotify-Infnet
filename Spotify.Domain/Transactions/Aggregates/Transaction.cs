using Spotify.Domain.Accounts.Aggregates;
using Spotify.Domain.Transactions.ValueObjects;

namespace Spotify.Domain.Transactions.Aggregates;

public class Transaction
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Merchant Merchant { get; set; }
    public double Amount { get; set; }
    public DateTime PurchasedDate { get; set; }
    public CreditCard CreditCard { get; set; }
}