﻿namespace Spotify.Streaming.Application.Bands.Dtos;
public class CreateAlbumDto
{
    public string Name { get; set; }
    public List<CreateMusicDto> Musics { get; set; }
    public DateTime ReleaseYear { get; set; }
}