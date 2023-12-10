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

    public Band GetBandById(Guid id)
    {
        return Bands.FirstOrDefault(b => b.Id == id);
    }

    public Music GetMusicById(Guid musicId)
    {
        return Bands.Select(b => 
                            b.Albums.Select(a => 
                                            a.Musics.FirstOrDefault(m => m.Id == musicId))
                            .FirstOrDefault())
            .FirstOrDefault();
    }
}