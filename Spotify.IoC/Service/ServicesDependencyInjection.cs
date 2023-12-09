using Microsoft.Extensions.DependencyInjection;
using Spotify.Application.Users;

namespace Spotify.IoC.Service;

public static class ServicesDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }
}