namespace Spotify.Streaming.Domain.Streaming.Aggregates;

public class Band
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Album> Albums { get; set; }

    public Band()
    {
        Albums = new List<Album>();
    }

    public void Create(string name, List<Album>? albums)
    {
        Name = name;

        foreach (var album in albums)
        { 
            Albums.Add(album);
        }

    }
}