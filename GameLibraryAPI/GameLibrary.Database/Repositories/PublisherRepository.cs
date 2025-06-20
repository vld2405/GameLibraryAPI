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
    public class PublisherRepository : BaseRepository<Publisher>
    {
        public PublisherRepository(GameLibraryDatabaseContext dbContext) : base(dbContext)
        {
        }
        public async Task<Publisher?> GetPublisherAsync(int id)
        {
            return await GetRecords().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return await GetRecords().ToListAsync();
        }
        public async Task<(IEnumerable<Publisher> publishers, int totalCount)> GetPublishersAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Publisher> query = GetRecords().Include(p => p.Games);
            var totalCount = await query.CountAsync();
            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            var publishers = await query.ToListAsync();
            return (publishers, totalCount);
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync(string? name = null, string? sortOrder = "asc")
        {
            IQueryable<Publisher> query = GetRecords().Include(p => p.Games);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            query = sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name);

            return await query.ToListAsync();
        }

        public async Task AddPublisherAsync(Publisher entity)
        {
            Insert(entity);
            await SaveChangesAsync();
        }

        public async Task SoftDeletePublisherAsync(int id)
        {
            var publisher = await GetFirstOrDefaultAsync(id);
            if (publisher == null)
                throw new KeyNotFoundException($"Publisher with ID {id} not found.");

            SoftDelete(publisher);
            await SaveChangesAsync();
        }
    }
}
