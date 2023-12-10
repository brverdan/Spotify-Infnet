using Spotify.Streaming.Domain.Streaming.ValueObjects;

namespace Spotify.Streaming.Domain.Streaming.Aggregates;

public class Music
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Album? Album { get; set; }
    public DateTime ReleaseYear { get; set; }
    public Duration Duration { get; set; }

    public void Create(string name, DateTime releaseYear, int duration)
    {
        Id = Guid.NewGuid();
        Name = name;
        ReleaseYear = releaseYear;
        Duration = new Duration(duration);
    }

    public void BelongsAlbum(Album album)
    {
        Album = album;
    }
}