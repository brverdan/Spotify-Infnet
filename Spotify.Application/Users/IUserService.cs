using Spotify.Domain.Accounts.Aggregates;

namespace Spotify.Application.Users;

public interface IUserService
{
    User GetUserById(Guid id);
    User CreateUser(CreateUserDto userCreateDto);
}