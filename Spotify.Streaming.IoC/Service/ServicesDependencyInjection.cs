using Microsoft.Extensions.DependencyInjection;
using Spotify.Streaming.Application.Bands;
using Spotify.Streaming.Application.Interfaces;
using Spotify.Streaming.Application.Plans;

namespace Spotify.Streaming.IoC.Service;

public static class ServicesDependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IBandService, BandService>();
    }
}