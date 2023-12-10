using Spotify.Streaming.Domain.Streaming.Aggregates;
using Spotify.Streaming.Infrastructure.Interfaces;

namespace Spotify.Streaming.Infrastructure.Repository;
public class PlanRepository : IPlanRepository
{
    private static List<Plan> Plans = new();

    public PlanRepository()
    {
        CreatePlans();
    }

    public Plan GetPlanById(Guid id)
    {
        return Plans.FirstOrDefault(u => u.Id == id);
    }

    public void CreatePlans()
    {
        if (Plans.Count == 0)
        {
            Plans.Add(new Plan
            {
                Id = new Guid("AA412963-68AA-4A7D-B1DC-5260E6C55A94"),
                Name = "Basic",
                Description = "Basic plan",
                Value = 50.0
            });
        }
    }
}