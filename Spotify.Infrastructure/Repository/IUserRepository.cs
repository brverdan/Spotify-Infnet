using Spotify.Domain.Accounts.Aggregates;

namespace Spotify.Infrastructure.Repository;

public interface IUserRepository
{
    User GetUserById(Guid id);
    void CreateUser(User createUserDto);
}