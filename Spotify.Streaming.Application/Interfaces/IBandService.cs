using Spotify.Streaming.Application.Bands.Dtos;
using Spotify.Streaming.Application.Bands.Dtos.Response;

namespace Spotify.Streaming.Application.Interfaces;

public interface IBandService
{
    ResponseBandDto CreateBand(CreateBandDto createBandDto);
    ResponseBandDto GetBandById(Guid id);
    ResponseMusicDto GetMusicById(Guid id);
}