using Spotify.Application.Interfaces;
using Spotify.Application.Users.Dtos;
using Spotify.Core.Exceptions;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Infrastructure.Interfaces;

namespace Spotify.Application.Users;

public class UserService : IUserService
{
    private IUserRepository UserRepository { get; set; }
    private IPlanRepository PlanRepository { get; set; }
    private IBandRepository BandRepository { get; set; }

    public UserService(IUserRepository userRepository,
                       IPlanRepository planRepository,
                       IBandRepository bandRepository)
    {
        UserRepository = userRepository;
        PlanRepository = planRepository;
        BandRepository = bandRepository;
    }

    public User GetUserById(Guid id)
    {
        return UserRepository.GetUserById(id);
    }

    public async Task<User> CreateUser(CreateUserDto createUserDto)
    {
        var plan = await PlanRepository.GetPlanById(createUserDto.Plan);

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

    public async Task<Playlist> AddMusic(Guid playlistId, Guid musicId)
    {
        var playlist = UserRepository.GetPlaylistById(playlistId);

        if (playlist == null)
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "Playlist not found",
                ErrorName = nameof(AddMusic)
            }).ValidateAndThrow();
        }

        var music = await BandRepository.GetMusicById(musicId);

        if (music == null)
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "Music not found",
                ErrorName = nameof(AddMusic)
            }).ValidateAndThrow();
        }

        playlist.AddMusic(music);

        return playlist;
    }

    public Playlist GetPlaylistById(Guid id)
    {
        var playlist = UserRepository.GetPlaylistById(id);

        if (playlist == null)
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "Playlist not found",
                ErrorName = nameof(GetPlaylistById)
            }).ValidateAndThrow();
        }

        return playlist;
    }

    public async Task<User> AddFavoriteMusic(Guid userId, Guid musicId)
    {
        var user = UserRepository.GetUserById(userId);

        if (user == null)
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "User not found",
                ErrorName = nameof(AddFavoriteMusic)
            }).ValidateAndThrow();
        }
        
        var music = await BandRepository.GetMusicById(musicId);

        if (music == null)
        {
            new BusinessException(new BusinessValidation
            {
                ErrorMessage = "Music not found",
                ErrorName = nameof(AddMusic)
            }).ValidateAndThrow();
        }

        user.AddFavoriteMusic(music);

        return user;
    }
}