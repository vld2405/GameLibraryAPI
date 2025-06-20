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
        public async Task<(IEnumerable<Genre> Genres, int TotalCount)> GetGenresAsync(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var query = GetRecords()
                .Include(d => d.Games)
                .OrderBy(d => d.Id);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
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

        public async Task SoftDeleteGenreAsync(int id)
        {
            var genre = await GetFirstOrDefaultAsync(id);
            if (genre == null)
                throw new KeyNotFoundException($"Genre with ID {id} not found.");

            SoftDelete(genre);
            await SaveChangesAsync();
        }
    }
}
