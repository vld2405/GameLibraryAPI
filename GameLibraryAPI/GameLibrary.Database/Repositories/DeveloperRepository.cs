﻿using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;
using GameLibrary.Infrastructure.Exceptions;
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
            var result = await GetRecords().Include(d => d.Games).FirstOrDefaultAsync(d => d.Id == id);

            if (result == null)
            {
                throw new NotFoundException($"Developer with ID {id} not found.");
            }

            return result;
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

        public async Task<IEnumerable<Developer>> GetDevelopersFilteredAsync(string? name = null, string? sortOrder = "asc")
        {
            IQueryable<Developer> query = GetRecords();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.Name.ToLower().Contains(name.ToLower()));
            }

            query = sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(d => d.Name)
                : query.OrderBy(d => d.Name);

            return await query.Include(d => d.Games).ToListAsync();
        }

        public async Task<(IEnumerable<Developer> Developers, int TotalCount)> GetDevelopersPaginatedAndFilteredAsync(int pageNumber, int pageSize, string? name = null, string? sortOrder = "asc")
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var query = GetRecords().Include(d => d.Games).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => d.Name.ToLower().Contains(name.ToLower()));
            }

            query = sortOrder?.ToLower() == "desc"
                ? query.OrderByDescending(d => d.Name)
                : query.OrderBy(d => d.Name);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
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
            {
                throw new NotFoundException($"Developer with ID {id} not found.");
            }

            SoftDelete(developer);
            await SaveChangesAsync();
        }


        public async Task UpdateDeveloperAsync(int id, Developer updatedEntity)
        {
            var currentDeveloper = await GetDeveloperByIdAsync(id);

            if (currentDeveloper == null)
            {
                throw new NotFoundException($"Developer with ID {id} not found.");
            }

            if (!string.IsNullOrEmpty(updatedEntity.Name))
            {
                currentDeveloper.Name = updatedEntity.Name;
            }

            Update(currentDeveloper);
            await SaveChangesAsync();
        }
        public async Task<List<Developer>> GetDevelopersByIdsAsync(List<int> devIds)
        {
            return await GetRecords()
                .Where(d => devIds.Contains(d.Id) && d.DeletedAt == null)
                .ToListAsync();
        }
    }
}
