using Spotify.Domain.Transactions.Aggregates;

namespace Spotify.Domain.Accounts.Aggregates;

public class CreditCard
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public string Username { get; set; }
    public User User { get; set; }
    public long AvailableLimit { get; set; }
    public DateTime ValidDate { get; set; }
    public int SecureCode { get; set; }
    public bool ActiveCard { get; set; }
    public IEnumerable<Transaction> Transactions { get; set; }
}