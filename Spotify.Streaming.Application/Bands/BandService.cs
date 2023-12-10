using Spotify.Streaming.Application.Bands.Dtos;
using Spotify.Streaming.Application.Bands.Dtos.Response;
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

    public ResponseBandDto CreateBand(CreateBandDto createBandDto)
    {
        var musics = new List<Music>();
        var albums = new List<Album>();

        if (createBandDto.Albums.Any())
        {
            foreach (var albumDto in createBandDto.Albums)
            {
                var album = new Album();

                album.Create(albumDto.Name, albumDto.ReleaseYear);

                albums.Add(album);

                var musicsAlbum = new List<Music>();

                foreach (var musicDto in albumDto.Musics)
                {
                    var music = new Music();
                    music.Create(musicDto.Name, musicDto.ReleaseYear, musicDto.Duration);

                    music.BelongsAlbum(album);

                    musicsAlbum.Add(music);

                    if (!musics.Contains(music))
                    {
                        musics.Add(music);
                    }
                }

                album.AddMusics(musicsAlbum);
            }
        }

        var band = new Band();
        band.Create(createBandDto.Name, albums);

        BandRepository.CreateBand(band);

        var result = MappingBand(band);

        return result;
    }

    public ResponseBandDto GetBandById(Guid id)
    {
        var band = BandRepository.GetBandById(id);

        var result = MappingBand(band);

        return result;
    }

    public ResponseMusicDto GetMusicById(Guid musicId)
    {
        var music = BandRepository.GetMusicById(musicId);

        var result = MappingMusic(music);

        return result;
    }

    private ResponseBandDto MappingBand(Band band)
    {
        var bandDto = new ResponseBandDto();

        bandDto.Id = band.Id;
        bandDto.Name = band.Name;
        bandDto.Albums = new List<ResponseAlbumDto>();

        foreach (var album in band.Albums)
        {
            var albumDto = new ResponseAlbumDto();
            albumDto.Id = album.Id;
            albumDto.Name = album.Name;
            albumDto.ReleaseYear = album.ReleaseYear;
            albumDto.Musics = new List<ResponseMusicDto>();


            foreach (var music in album.Musics)
            {
                var musicDto = new ResponseMusicDto();
                musicDto.Id = music.Id;
                musicDto.Name = music.Name;
                musicDto.ReleaseYear = music.ReleaseYear;
                musicDto.Duration = music.Duration.Value;

                albumDto.Musics.Add(musicDto);
            }

            bandDto.Albums.Add(albumDto);
        }

        return bandDto;
    }

    private ResponseMusicDto MappingMusic(Music music)
    {
        return new ResponseMusicDto()
        {
            Id = music.Id,
            Name = music.Name,
            ReleaseYear = music.ReleaseYear,
            Duration = music.Duration.Value
        };
    }

}