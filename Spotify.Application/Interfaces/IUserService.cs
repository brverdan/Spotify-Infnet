using Spotify.Application.Users.Dtos;
using Spotify.Domain.Accounts.Aggregates;

namespace Spotify.Application.Interfaces;

public interface IUserService
{
    User GetUserById(Guid id);
    Task<User> CreateUser(CreateUserDto userCreateDto);
    Playlist CreatePlaylist(Guid userId, PlaylistDto playlistDto);
    Task<Playlist> AddMusic(Guid playlistId, Guid musicId);
}