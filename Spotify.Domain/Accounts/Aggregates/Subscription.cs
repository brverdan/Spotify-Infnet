using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.Domain.Accounts.Aggregates;

public class Subscription
{
    public Guid Id { get; set; }
    public DateTime ValidDateAt { get; set; }
    public Plan Plan { get; set; }
    public bool Active{ get; set; }
}