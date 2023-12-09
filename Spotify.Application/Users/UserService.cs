using Spotify.Core.Exceptions;
using Spotify.Domain.Accounts.Aggregates;
using Spotify.Infrastructure.Repository;

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
}