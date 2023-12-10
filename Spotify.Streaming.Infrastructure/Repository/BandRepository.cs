using Spotify.Streaming.Domain.Streaming.Aggregates;
using Spotify.Streaming.Infrastructure.Interfaces;

namespace Spotify.Streaming.Infrastructure.Repository;
public class BandRepository : IBandRepository
{
    private static List<Band> Bands = new List<Band>();

    public void CreateBand(Band band)
    {
        band.Id = Guid.NewGuid();
        Bands.Add(band);
    }

    public Music GetMusicById(Guid musicId)
    {
        return Bands.Select(b => b.Musics.FirstOrDefault(m => m.Id == musicId)).FirstOrDefault();
    }
}