using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.Infrastructure.Interfaces;

public interface IBandRepository
{
    void CreateBand(Band band);
    Band GetBandById(Guid id);
    Music GetMusicById(Guid id);
}