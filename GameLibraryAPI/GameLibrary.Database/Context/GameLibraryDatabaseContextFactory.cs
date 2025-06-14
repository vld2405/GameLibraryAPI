using GameLibrary.Infrastructure.Config;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Context
{
    public class GameLibraryDatabaseContextFactory : IDesignTimeDbContextFactory<GameLibraryDatabaseContext>
    {
        public GameLibraryDatabaseContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}")
                .AddJsonFile($"appsettings.Development.json");

            var configuration = builder.Build();
            AppConfig.Init(configuration);

            return new GameLibraryDatabaseContext();
        }
    }
}
