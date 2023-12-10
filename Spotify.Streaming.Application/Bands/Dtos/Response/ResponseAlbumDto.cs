using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.Application.Bands.Dtos.Response;

public class ResponseAlbumDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ResponseMusicDto> Musics { get; set; }
    public DateTime ReleaseYear { get; set; }
}