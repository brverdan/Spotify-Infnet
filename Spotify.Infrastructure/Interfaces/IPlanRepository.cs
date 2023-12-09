using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.Infrastructure.Interfaces;
public interface IPlanRepository
{
    Plan GetPlanById(Guid id);
}