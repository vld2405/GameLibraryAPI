using GameLibrary.Database.Entities;
using GameLibrary.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Database.Context
{
    public class GameLibraryDatabaseContext : DbContext
    {
        public GameLibraryDatabaseContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfig.ConnectionStrings?.GameLibraryDatabase);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
