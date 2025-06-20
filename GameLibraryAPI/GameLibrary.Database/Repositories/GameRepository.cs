using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(GameLibraryDatabaseContext dbContext) : base(dbContext)
        {
        }
        public async Task<Game?> GetGameAsync(int id)
        {
            return await GetRecords().FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await GetRecords().ToListAsync();
        }
        public async Task<(IEnumerable<Game> games, int totalCount)> GetGamesAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Game> query = GetRecords().Include(g => g.Users).Include(g => g.Types).Include(g => g.Developers).Include(g => g.Publishers);
            var totalCount = await query.CountAsync();
            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            var games = await query.ToListAsync();
            return (games, totalCount);
        }
        public async Task<IEnumerable<Game>> GetGamesAsync(string? name = null, DateTime? releaseDate = null, int? typeId = null, int? developerId = null, int? publisherId = null, int? userId = null, string? sortBy = null, string? sortOrder = "asc")
        {
            IQueryable<Game> query = GetRecords().Include(g => g.Types).Include(g => g.Developers).Include(g => g.Publishers).Include(g => g.Users);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(g => g.Name.ToLower().Contains(name.ToLower()));

            if (releaseDate.HasValue)
                query = query.Where(g => g.ReleaseDate.Date == releaseDate.Value.Date);

            if (typeId.HasValue)
                query = query.Where(g => g.Types.Any(t => t.Id == typeId));

            if (developerId.HasValue)
                query = query.Where(g => g.Developers.Any(d => d.Id == developerId));

            if (publisherId.HasValue)
                query = query.Where(g => g.Publishers.Any(p => p.Id == publisherId));

            if (userId.HasValue)
                query = query.Where(g => g.Users.Any(u => u.Id == userId));

            switch (sortBy?.ToLower())
            {
                case "name":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Name)
                        : query.OrderBy(g => g.Name);
                    break;

                case "releasedate":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.ReleaseDate)
                        : query.OrderBy(g => g.ReleaseDate);
                    break;

                case "type":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Types.Select(t => t.Name).FirstOrDefault())
                        : query.OrderBy(g => g.Types.Select(t => t.Name).FirstOrDefault());
                    break;

                case "developer":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Developers.Select(d => d.Name).FirstOrDefault())
                        : query.OrderBy(g => g.Developers.Select(d => d.Name).FirstOrDefault());
                    break;

                case "publisher":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Publishers.Select(p => p.Name).FirstOrDefault())
                        : query.OrderBy(g => g.Publishers.Select(p => p.Name).FirstOrDefault());
                    break;

                case "user":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Users.Select(u => u.Username).FirstOrDefault())
                        : query.OrderBy(g => g.Users.Select(u => u.Username).FirstOrDefault());
                    break;

                default:
                    query = query.OrderBy(g => g.Id);
                    break;
            }

            return await query.ToListAsync();
        }


    }
}
