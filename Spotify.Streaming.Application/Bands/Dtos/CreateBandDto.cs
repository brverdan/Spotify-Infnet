using Spotify.Streaming.Domain.Streaming.Aggregates;
using System.ComponentModel.DataAnnotations;

namespace Spotify.Streaming.Application.Bands.Dtos;

public class CreateBandDto
{
    [Required]
    public string Name { get; set; }

    public List<CreateAlbumDto>? Albums { get; set; }
    public List<CreateMusicDto>? Musics { get; set; }
}