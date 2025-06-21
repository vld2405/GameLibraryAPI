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
    public class DeveloperRepository : BaseRepository<Developer>
    {
        public DeveloperRepository(GameLibraryDatabaseContext dbContext) : base(dbContext)
        {}

        public async Task<Developer?> GetDeveloperByIdAsync(int id)
        {
            return await GetRecords().Include(d => d.Games).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Developer>> GetDevelopersAsync()
        {
            return await GetRecords().Include(d => d.Games).ToListAsync();
        }

        public async Task<(IEnumerable<Developer> Developers, int TotalCount)> GetDevelopersAsync(int pageNumber = 1, int pageSize = 10)
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

        public async Task<IEnumerable<Developer>> GetDevelopersAsync(string? name = null, string? sortOrder = "asc")
        {
            IQueryable<Developer> query = GetRecords().Include(d=>d.Games);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.Name.ToLower().Contains(name.ToLower()));
            }

            query = sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(d => d.Name)
                : query.OrderBy(d => d.Name);

            return await query.ToListAsync();
        }

        public async Task AddDevAsync(Developer entity)
        {
            Insert(entity);
            await SaveChangesAsync();
        }

        public async Task SoftDeleteDevAsync(int id)
        {
            var developer = await GetFirstOrDefaultAsync(id);
            if (developer == null)
                throw new KeyNotFoundException($"Developer with ID {id} not found.");

            SoftDelete(developer);
            await SaveChangesAsync();
        }

    }
}
