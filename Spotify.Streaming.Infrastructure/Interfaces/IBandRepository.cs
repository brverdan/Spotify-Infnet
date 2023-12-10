using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.Infrastructure.Interfaces;

public interface IBandRepository
{
    void CreateBand(Band band);
    Music GetMusicById(Guid id);
}