﻿using Spotify.Application.Users.Dtos;
using Spotify.Domain.Accounts.Aggregates;

namespace Spotify.Application.Interfaces;

public interface IUserService
{
    User GetUserById(Guid id);
    Task<User> CreateUser(CreateUserDto userCreateDto);
    Playlist CreatePlaylist(Guid userId, PlaylistDto playlistDto);
    Playlist GetPlaylistById(Guid id);
    Task<Playlist> AddMusic(Guid playlistId, Guid musicId);
    Task<User> AddFavoriteMusic(Guid userId, Guid musicId);
}