using Azure.Core;
using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;
using GameLibrary.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
        {}

        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            var result = await GetRecords().Include(p => p.Games).FirstOrDefaultAsync(p => p.Id == id);

            if (result == null)
            {
                throw new NotFoundException($"Publisher with ID {id} not found.");
            }

            return result;
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return await GetRecords().Include(p => p.Games).ToListAsync();
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

        public async Task<IEnumerable<Publisher>> GetPublishersFilteredAsync(string? name = null, string? sortOrder = "asc")
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
            {
                throw new NotFoundException($"Publisher with ID {id} not found.");
            }

            SoftDelete(publisher);
            await SaveChangesAsync();
        }

        public async Task UpdatePublisherAsync(int id, Publisher updatedEntity)
        {
            var currentPublisher = await GetPublisherByIdAsync(id);

            if (currentPublisher == null)
            {
                throw new NotFoundException($"Publisher with ID {id} not found.");
            }

            if (!string.IsNullOrEmpty(updatedEntity.Name))
            {
                currentPublisher.Name = updatedEntity.Name;
            }

            Update(currentPublisher);
            await SaveChangesAsync();
        }
    }
}
