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
    public class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(GameLibraryDatabaseContext dbContext) : base(dbContext)
        {
        }
        public async Task<Genre?> GetGenresAsync(int id)
        {
            return await GetRecords().FirstOrDefaultAsync(nt => nt.Id == id);
        }
        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await GetRecords().ToListAsync();
        }
        public async Task<(IEnumerable<Genre> genres, int totalCount)> GetGenresAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Genre> query = GetRecords();

            var totalCount = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var genres = await query.ToListAsync();

            return (genres, totalCount);
        }
    }
}
