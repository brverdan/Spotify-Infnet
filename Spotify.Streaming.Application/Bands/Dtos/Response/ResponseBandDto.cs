using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.Application.Bands.Dtos.Response;
public class ResponseBandDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<ResponseAlbumDto> Albums { get; set; }
}