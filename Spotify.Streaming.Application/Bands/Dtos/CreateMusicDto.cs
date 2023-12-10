namespace Spotify.Streaming.Application.Bands.Dtos;
public class CreateMusicDto
{
    public string Name { get; set; }
    public Guid? Album { get; set; }
    public DateTime ReleaseYear { get; set; }
    public int Duration { get; set; }
}