using GameLibrary.Database.Context;
using GameLibrary.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database
{
    public static class DIConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<GameLibraryDatabaseContext>();
            services.AddScoped<DbContext, GameLibraryDatabaseContext>();

            services.AddScoped<DeveloperRepository>();
            services.AddScoped<PublisherRepository>();
            services.AddScoped<GameRepository>();
            services.AddScoped<GenreRepository>();
            services.AddScoped<UserRepository>();

            return services;
        }
    }
}
