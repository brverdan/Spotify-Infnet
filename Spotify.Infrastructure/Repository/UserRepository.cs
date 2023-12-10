using Spotify.Domain.Accounts.Aggregates;
using Spotify.Infrastructure.Interfaces;

namespace Spotify.Infrastructure.Repository;
public class UserRepository : IUserRepository
{
    private static List<User> Users = new();

    public User GetUserById(Guid id)
    {
        return Users.FirstOrDefault(u => u.Id == id);
    }

    public void CreateUser(User user)
    {
        user.Id = Guid.NewGuid();
        Users.Add(user);
    }

    public void CreatePlaylist(Guid userId, Playlist playlist)
    {
        var user = Users.FirstOrDefault(u => u.Id == userId);
        user.Playlists.Add(playlist);
    }

    public Playlist GetPlaylistById(Guid playlistId)
    {
        return Users.Select(u => u.Playlists.FirstOrDefault(p => p.Id == playlistId)).FirstOrDefault();
    }
}