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
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await GetRecords().Include(u => u.Games).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await GetRecords().Include(u => u.Games).ToListAsync();
        }
        public async Task<(IEnumerable<User> users, int totalCount)> GetUsersAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<User> query = GetRecords().Include(u => u.Games);
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

        public async Task<IEnumerable<User>> GetUsersAsync(string? username = null, string? email = null, string? sortBy = null, string? sortOrder = "asc")
        {
            IQueryable<User> query = GetRecords().Include(u => u.Games); 

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => u.Username.ToLower().Contains(username.ToLower()));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email.ToLower().Contains(email.ToLower()));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "username":
                        query = sortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(u => u.Username)
                            : query.OrderBy(u => u.Username);
                        break;

                    case "email":
                        query = sortOrder?.ToLower() == "desc"
                            ? query.OrderByDescending(u => u.Email)
                            : query.OrderBy(u => u.Email);
                        break;

                    default:
                        query = query.OrderBy(u => u.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(u => u.Id);
            }

            return await query.ToListAsync();
        }


    }
}
