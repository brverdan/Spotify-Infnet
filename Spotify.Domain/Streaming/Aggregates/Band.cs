namespace Spotify.Domain.Streaming.Aggregates;

public class Band
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Music> Musics { get; set; }
    public IEnumerable<Album> Albums { get; set; }
}