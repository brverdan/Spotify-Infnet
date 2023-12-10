using Spotify.Streaming.Application.Bands.Dtos;
using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.Application.Interfaces;

public interface IBandService
{
    Band CreateBand(CreateBandDto createBandDto);
    Music GetMusicById(Guid id);
}