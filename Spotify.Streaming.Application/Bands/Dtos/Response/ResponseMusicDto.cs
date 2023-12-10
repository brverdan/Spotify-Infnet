namespace Spotify.Streaming.Application.Bands.Dtos.Response;

public class ResponseMusicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseYear { get; set; }
    public int Duration { get; set; }
}