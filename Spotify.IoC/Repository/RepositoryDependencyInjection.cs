﻿using Microsoft.Extensions.DependencyInjection;
using Spotify.Infrastructure.Interfaces;
using Spotify.Infrastructure.Repository;

namespace Spotify.IoC.Repository;

public static class RepositoryDependencyInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IBandRepository, BandRepository>();
    }
}