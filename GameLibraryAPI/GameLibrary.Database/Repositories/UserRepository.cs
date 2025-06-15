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
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(GameLibraryDatabaseContext dbContext) : base(dbContext)
        {
        }
        public async Task<User?> GetUserAsync(int id)
        {
            return await GetRecords().FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await GetRecords().ToListAsync();
        }
        public async Task<(IEnumerable<User> users, int totalCount)> GetUsersAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<User> query = GetRecords();
            var totalCount = await query.CountAsync();
            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            var users = await query.ToListAsync();
            return (users, totalCount);
        }
    }
}
