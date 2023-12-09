using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.Domain.Accounts.Aggregates;

public class Playlist
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Public { get; set; }
    public List<Music> Musics { get; set; }
    public DateTime CreatedAt { get; set; }

    public Playlist()
    {
        Musics = new List<Music>();
    }

    public void CreatePlaylist(string name, bool visibility)
    {
        Id = Guid.NewGuid();
        Name = name;
        Public = visibility;
        CreatedAt = DateTime.Now;
    }
}