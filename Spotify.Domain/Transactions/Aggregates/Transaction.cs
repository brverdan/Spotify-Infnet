using Spotify.Domain.Transactions.ValueObjects;

namespace Spotify.Domain.Transactions.Aggregates;

public class Transaction
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public Merchant Merchant { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }
    public DateTime PurchasedDate { get; set; }


    public void Create(string username, double amount, string planName)
    {
        Username = username;
        Merchant = new Merchant { MerchantName = "Spotify" };
        Amount = amount;
        PurchasedDate = DateTime.Now;
        Description = $"{planName} purchase";
    }
}