using Microsoft.Extensions.DependencyInjection;
using GameLibrary.Core.Services;

namespace GameLibrary.Core;

public static class DIConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<DevelopersService>();
        services.AddScoped<PublisherService>();
        services.AddScoped<GamesService>();
        services.AddScoped<GenresService>();
        services.AddScoped<UsersService>();
        services.AddScoped<AuthService>();

        return services;
    }
}
