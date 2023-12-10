namespace Spotify.Streaming.Domain.Streaming.Aggregates;

public class Band
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Music> Musics { get; set; }
    public List<Album> Albums { get; set; }

    public Band()
    {
        Musics = new List<Music>();
        Albums = new List<Album>();
    }

    public void Create(string name, List<Music>? musics, List<Album>? albums)
    {
        Name = name;

        foreach (var album in albums)
        { 
            AddMusic(album.Musics);

            Albums.Add(album);
        }

        AddMusic(musics);
    }

    public void AddMusic(IEnumerable<Music> musics)
    {
        foreach (var music in musics)
        {
            if (!Musics.Contains(music))
            {
                Musics.Add(music);
            }
        }
    }
}