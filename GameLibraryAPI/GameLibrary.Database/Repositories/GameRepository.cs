using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(Context.GameLibraryDatabaseContext dbContext) : base(dbContext)
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
            IQueryable<Game> query = GetRecords();
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
    }
}
