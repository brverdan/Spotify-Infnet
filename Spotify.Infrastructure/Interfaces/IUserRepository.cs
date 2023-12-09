using Spotify.Domain.Accounts.Aggregates;

namespace Spotify.Infrastructure.Interfaces;

public interface IUserRepository
{
    User GetUserById(Guid id);
    void CreateUser(User user);
    void CreatePlaylist(Guid userId, Playlist playlist);
}