using Spotify.Domain.Accounts.Aggregates;

namespace Spotify.Domain.Streaming.Aggregates;

public class Playlist
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Public { get; set; }
    public User user { get; set; }
    public IEnumerable<Music> Musics { get; set; }
    public DateTime CreatedAt { get; set; }
}