using Spotify.Domain.Accounts.ValueObjects;
using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.Domain.Accounts.Aggregates;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public CPF Cpf { get; set; }
    public IEnumerable<CreditCard> CreditCards { get; set; }
    public IEnumerable<Band> Bands { get; set; }
    public IEnumerable<Playlist> Playlists { get; set; }
    public IEnumerable<Subscription> Subscriptions { get; set; }
}