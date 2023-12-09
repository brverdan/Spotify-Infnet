using System.ComponentModel.DataAnnotations;

namespace Spotify.Application.Users.Dtos;
public class PlaylistDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public bool Visibility { get; set; }
}