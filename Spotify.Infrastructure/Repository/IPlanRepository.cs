using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.Infrastructure.Repository;
public interface IPlanRepository
{
    Plan GetPlanById(Guid id);
}