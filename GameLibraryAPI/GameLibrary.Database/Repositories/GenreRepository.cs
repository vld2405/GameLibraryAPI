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
            return await GetRecords().FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await GetRecords().ToListAsync();
        }
        public async Task<(IEnumerable<Genre> genres, int totalCount)> GetGenresAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Genre> query = GetRecords().Include(g => g.Games);

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

        public async Task<IEnumerable<Genre>> GetGenresAsync(string? name = null, string? sortOrder = "asc")
        {
            IQueryable<Genre> query = GetRecords().Include(g => g.Games);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(g => g.Name.ToLower().Contains(name.ToLower()));
            }

            query = sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(g => g.Name)
                : query.OrderBy(g => g.Name);

            return await query.ToListAsync();
        }

        public async Task AddGenreAsync(Genre entity)
        {
            Insert(entity);
            await SaveChangesAsync();
        }
    }
}
