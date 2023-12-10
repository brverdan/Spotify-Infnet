using Newtonsoft.Json;
using Spotify.Domain.Aggregates;
using Spotify.Infrastructure.Interfaces;

namespace Spotify.Infrastructure.Repository;
public class BandRepository : IBandRepository
{
    private readonly HttpClient HttpClient;

    public BandRepository(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<Music> GetMusicById(Guid musicId)
    {
        var result = await HttpClient.GetAsync($"https://localhost:7027/api/Band/music/{musicId}");

        if (result.IsSuccessStatusCode == false)
            return null;

        var content = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Music>(content);
    }
}