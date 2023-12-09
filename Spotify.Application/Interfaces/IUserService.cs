using Spotify.Application.Users.Dtos;
using Spotify.Domain.Accounts.Aggregates;

namespace Spotify.Application.Interfaces;

public interface IUserService
{
    User GetUserById(Guid id);
    User CreateUser(CreateUserDto userCreateDto);
    Playlist CreatePlaylist(Guid userId, PlaylistDto playlistDto);
}