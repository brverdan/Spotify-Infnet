using Spotify.Application.Interfaces;
using Spotify.Application.Users.Dtos;
using Spotify.Core.Exceptions;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Infrastructure.Interfaces;

namespace Spotify.Application.Users;

public class UserService : IUserService
{
    public IUserRepository UserRepository { get; set; }
    public IPlanRepository PlanRepository { get; set; }

    public UserService(IUserRepository userRepository,
                       IPlanRepository planRepository)
    {
        UserRepository = userRepository;
        PlanRepository = planRepository;
    }

    public User GetUserById(Guid id)
    {
        return UserRepository.GetUserById(id);
    }

    public User CreateUser(CreateUserDto createUserDto)
    {
        var plan = PlanRepository.GetPlanById(createUserDto.Plan);

        if (plan == null)
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "Plan not found",
                ErrorName = nameof(CreateUser)
            }).ValidateAndThrow();
        }

        var creditCard = new CreditCard();
        creditCard.CreateCard(createUserDto.CreditCard.Number,
                              createUserDto.CreditCard.AvailableLimit,
                              createUserDto.CreditCard.ActiveCard,
                              createUserDto.CreditCard.SecureCode,
                              createUserDto.Name);

        var user = new User();
        user.Create(createUserDto.Name, createUserDto.Email, createUserDto.Cpf, creditCard, plan);

        UserRepository.CreateUser(user);
        return user;
    }

    public Playlist CreatePlaylist(Guid userId, PlaylistDto playlistDto)
    {
        var user = UserRepository.GetUserById(userId);

        if (user == null)
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "User not found",
                ErrorName = nameof(CreatePlaylist)
            }).ValidateAndThrow();
        }

        if (user.Playlists.Any(p => p.Name == playlistDto.Name))
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "User already has this playlist name, please choose another name",
                ErrorName = nameof(CreatePlaylist)
            }).ValidateAndThrow();
        }

        var playlist = new Playlist();

        playlist.CreatePlaylist(playlistDto.Name, playlistDto.Visibility);

        UserRepository.CreatePlaylist(user.Id, playlist);

        return playlist;
    }
}