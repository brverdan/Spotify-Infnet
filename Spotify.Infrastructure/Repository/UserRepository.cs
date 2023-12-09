using Spotify.Domain.Accounts.Aggregates;

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
}