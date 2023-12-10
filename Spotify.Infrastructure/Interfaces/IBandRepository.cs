using Spotify.Domain.Aggregates;

namespace Spotify.Infrastructure.Interfaces;

public interface IBandRepository
{
    Task<Music> GetMusicById(Guid id);
}