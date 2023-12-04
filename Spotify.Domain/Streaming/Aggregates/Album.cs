namespace Spotify.Domain.Streaming.Aggregates;

public class Album
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Band Band { get; set; }
    public IEnumerable<Music> Musics { get; set; }
    public DateTime CreationYear { get; set; }
}