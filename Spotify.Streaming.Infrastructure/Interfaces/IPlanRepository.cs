using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.Infrastructure.Interfaces;
public interface IPlanRepository
{
    Plan GetPlanById(Guid id);
}