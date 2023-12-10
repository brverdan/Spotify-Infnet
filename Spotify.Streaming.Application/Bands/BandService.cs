using Spotify.Streaming.Application.Bands.Dtos;
using Spotify.Streaming.Application.Interfaces;
using Spotify.Streaming.Domain.Streaming.Aggregates;
using Spotify.Streaming.Infrastructure.Interfaces;

namespace Spotify.Streaming.Application.Bands;
public class BandService : IBandService
{
    private IBandRepository BandRepository { get; set; }

    public BandService(IBandRepository bandRepository)
    {
        BandRepository = bandRepository;
    }

    public Band CreateBand(CreateBandDto createBandDto)
    {
        var musics = new List<Music>();
        var albums = new List<Album>();

        if (createBandDto.Musics.Any())
        {
            foreach (var musicDto in createBandDto.Musics)
            {
                var music = new Music();
                music.Create(musicDto.Name, musicDto.ReleaseYear, musicDto.Duration);

                musics.Add(music);
            }
        }

        if (createBandDto.Albums.Any())
        {
            foreach (var albumDto in albums)
            {
                var album = new Album();
                album.Create(albumDto.Name, albumDto.Musics, albumDto.ReleaseYear);

                albums.Add(album);
            }
        }

        var band = new Band();
        band.Create(createBandDto.Name, musics, albums);

        BandRepository.CreateBand(band);

        return band;
    }

    public Music GetMusicById(Guid musicId)
    {
        return BandRepository.GetMusicById(musicId);
    }
}