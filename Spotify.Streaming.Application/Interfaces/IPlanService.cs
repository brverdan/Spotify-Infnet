using Spotify.Streaming.Domain.Streaming.Aggregates;

namespace Spotify.Streaming.Application.Interfaces;

public interface IPlanService
{
    Plan GetPlanById(Guid id);
}