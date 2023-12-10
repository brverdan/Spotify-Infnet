using Spotify.Streaming.Application.Interfaces;
using Spotify.Streaming.Domain.Streaming.Aggregates;
using Spotify.Streaming.Infrastructure.Interfaces;

namespace Spotify.Streaming.Application.Plans;
public class PlanService : IPlanService
{
    private IPlanRepository PlanRepository { get; set; }

    public PlanService(IPlanRepository planRepository)
    {
        PlanRepository = planRepository;
    }

    public Plan GetPlanById(Guid id)
    {
        return PlanRepository.GetPlanById(id);
    }
}