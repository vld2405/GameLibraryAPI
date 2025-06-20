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

        public async Task<Developer?> GetDevelopersAsync(int id)
        {
            return await GetRecords().FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Developer>> GetDevelopersAsync()
        {
            return await GetRecords().ToListAsync();
        }

        public async Task<(IEnumerable<Developer> developers, int totalCount)> GetDevelopersAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Developer> query = GetRecords().Include(d => d.Games);

            var totalCount = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            var developers = await query.ToListAsync();

            return (developers, totalCount);
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

    }
}
