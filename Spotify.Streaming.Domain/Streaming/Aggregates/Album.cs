namespace Spotify.Streaming.Domain.Streaming.Aggregates;

public class Album
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Band Band { get; set; }
    public List<Music> Musics { get; set; }
    public DateTime ReleaseYear { get; set; }

    public void Create(string name, IEnumerable<Music> musics, DateTime releaseYear)
    {
        Id = Guid.NewGuid();
        Name = name;
        Musics.AddRange(musics);
        ReleaseYear = releaseYear;
    }
}