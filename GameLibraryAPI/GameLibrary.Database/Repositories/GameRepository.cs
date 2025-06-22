using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(GameLibraryDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await GetRecords()
                .Include(g => g.Developers)
                .Include(g => g.Publishers)
                .Include(g => g.Genres)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetGamesWithInfoAsync()
        {
            var games = await GetRecords()
                .Include(g => g.Developers)
                .Include(g => g.Publishers)
                .Include(g => g.Genres)
                .ToListAsync();

            return games;
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await GetRecords().ToListAsync();
        }

        public async Task<(IEnumerable<Game> games, int totalCount)> GetGamesAsync(int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<Game> query = GetRecords().Include(g => g.Users).Include(g => g.Genres).Include(g => g.Developers).Include(g => g.Publishers);
            var totalCount = await query.CountAsync();
            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            var games = await query.ToListAsync();
            return (games, totalCount);
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(string? name = null, DateTime? releaseDate = null, int? typeId = null, int? developerId = null, int? publisherId = null, int? userId = null, string? sortBy = null, string? sortOrder = "asc")
        {
            IQueryable<Game> query = GetRecords().Include(g => g.Genres).Include(g => g.Developers).Include(g => g.Publishers).Include(g => g.Users);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(g => g.Name.ToLower().Contains(name.ToLower()));

            if (releaseDate.HasValue)
                query = query.Where(g => g.ReleaseDate.Date == releaseDate.Value.Date);

            if (typeId.HasValue)
                query = query.Where(g => g.Genres.Any(t => t.Id == typeId));

            if (developerId.HasValue)
                query = query.Where(g => g.Developers.Any(d => d.Id == developerId));

            if (publisherId.HasValue)
                query = query.Where(g => g.Publishers.Any(p => p.Id == publisherId));

            if (userId.HasValue)
                query = query.Where(g => g.Users.Any(u => u.Id == userId));

            switch (sortBy?.ToLower())
            {
                case "name":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Name)
                        : query.OrderBy(g => g.Name);
                    break;

                case "releasedate":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.ReleaseDate)
                        : query.OrderBy(g => g.ReleaseDate);
                    break;

                case "type":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Genres.Select(t => t.Name).FirstOrDefault())
                        : query.OrderBy(g => g.Genres.Select(t => t.Name).FirstOrDefault());
                    break;

                case "developer":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Developers.Select(d => d.Name).FirstOrDefault())
                        : query.OrderBy(g => g.Developers.Select(d => d.Name).FirstOrDefault());
                    break;

                case "publisher":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Publishers.Select(p => p.Name).FirstOrDefault())
                        : query.OrderBy(g => g.Publishers.Select(p => p.Name).FirstOrDefault());
                    break;

                case "user":
                    query = sortOrder == "desc"
                        ? query.OrderByDescending(g => g.Users.Select(u => u.Username).FirstOrDefault())
                        : query.OrderBy(g => g.Users.Select(u => u.Username).FirstOrDefault());
                    break;

                default:
                    query = query.OrderBy(g => g.Id);
                    break;
            }

            return await query.ToListAsync();
        }

        public async Task<List<Developer>> GetAllDevsAsync(List<int> devIds)
        {
            return await _databaseContext.Developers
                .Where(d => devIds.Contains(d.Id) && d.DeletedAt == null)
                .ToListAsync();
        }
        public async Task<List<Publisher>> GetAllPublishersAsync(List<int> pubIds)
        {
            return await _databaseContext.Publishers
                .Where(p => pubIds.Contains(p.Id) && p.DeletedAt == null)
                .ToListAsync();
        }
        public async Task<List<Genre>> GetAllGenresAsync(List<int> genIds)
        {
            return await _databaseContext.Genres
                .Where(g => genIds.Contains(g.Id) && g.DeletedAt == null)
                .ToListAsync();
        }
        public async Task<List<User>> GetAllUsersAsync(List<int> userIds)
        {
            return await _databaseContext.Users
                .Where(u => userIds.Contains(u.Id) && u.DeletedAt == null)
                .ToListAsync();
        }

        public async Task AddGameAsync(Game entity, List<int> devIds, List<int> pubIds, List<int> genIds, List<int> userIds)
        {
            entity.Developers = await GetAllDevsAsync(devIds);
            entity.Publishers = await GetAllPublishersAsync(pubIds);
            entity.Genres = await GetAllGenresAsync(genIds);
            entity.Users = await GetAllUsersAsync(userIds);
            Insert(entity);
            await SaveChangesAsync();
        }

        public async Task SoftDeleteGameAsync(int id)
        {
            var game = await GetFirstOrDefaultAsync(id);
            if (game == null)
                throw new KeyNotFoundException($"Game with ID {id} not found.");

            SoftDelete(game);
            await SaveChangesAsync();
        }

        public async Task UpdateGameAsync(int id, Game updatedEntity, List<int>? devIds, List<int>? pubIds, List<int>? genIds, List<int>? userIds)
        {
            var currentGame= await GetGameByIdAsync(id);

            if (currentGame == null)
            {
                //Middleware
                //throw new NotFoundException(id);
                throw new KeyNotFoundException();
            }

            if (!string.IsNullOrEmpty(updatedEntity.Name))
            {
                currentGame.Name = updatedEntity.Name;
            }

            if (updatedEntity.ReleaseDate != default(DateTime))
            { 
                currentGame.ReleaseDate = updatedEntity.ReleaseDate; 
            }

            if (!string.IsNullOrEmpty(updatedEntity.Description))
            {
                currentGame.Description = updatedEntity.Description;
            }

            if(devIds != null)
            {
                currentGame.Developers = await GetAllDevsAsync(devIds);
            }

            if (pubIds != null)
            {
                currentGame.Publishers = await GetAllPublishersAsync(pubIds);
            }

            if (genIds != null)
            {
                currentGame.Genres = await GetAllGenresAsync(genIds);
            }

            if (userIds != null)
            {
                currentGame.Users = await GetAllUsersAsync(userIds);
            }

            Update(currentGame);
            await SaveChangesAsync();
        }
    }
}
