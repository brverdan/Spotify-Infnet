using Newtonsoft.Json;
using Spotify.Domain.Streaming.Aggregates;
using Spotify.Infrastructure.Interfaces;

namespace Spotify.Infrastructure.Repository;
public class PlanRepository : IPlanRepository
{
    private readonly HttpClient HttpClient;

    public PlanRepository()
    {
        HttpClient = new HttpClient();
    }

    public async Task<Plan> GetPlanById(Guid id)
    {
        var result = await HttpClient.GetAsync($"https://localhost:7027/api/plan/{id}");

        if (result.IsSuccessStatusCode == false)
            return null;

        var content = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Plan>(content);
    }
}