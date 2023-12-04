namespace Spotify.Domain.Streaming.Aggregates;

public class Music
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Band Band { get; set; }
    public Album Album { get; set; }
    public IEnumerable<Playlist> Playlists { get; set; }
    public DateTime CreationYear { get; set; }
}