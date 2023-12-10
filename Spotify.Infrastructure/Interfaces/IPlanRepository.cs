using Spotify.Domain.Streaming.Aggregates;

namespace Spotify.Infrastructure.Interfaces;
public interface IPlanRepository
{
    Task<Plan> GetPlanById(Guid id);
}