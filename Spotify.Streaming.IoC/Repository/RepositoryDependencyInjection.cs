using Microsoft.Extensions.DependencyInjection;
using Spotify.Streaming.Infrastructure.Interfaces;
using Spotify.Streaming.Infrastructure.Repository;

namespace Spotify.Streaming.IoC.Repository;

public static class RepositoryDependencyInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IBandRepository, BandRepository>();
    }
}