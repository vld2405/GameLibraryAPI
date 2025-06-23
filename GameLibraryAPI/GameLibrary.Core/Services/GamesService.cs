using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;
using System.ComponentModel;

namespace GameLibrary.Core.Services;

public class GamesService(GameRepository gameRepository,
                          DeveloperRepository devsRepository,
                          PublisherRepository publisherRepository,
                          GenreRepository genreRepository,
                          UserRepository userRepository
                          )
{
    public async Task<List<Developer>> GetAllDevsAsync(List<int> devIds)
    {
        return await devsRepository.GetDevelopersByIdsAsync(devIds);
    }
    public async Task<List<Publisher>> GetAllPublishersAsync(List<int> pubIds)
    {
        return await publisherRepository.GetPublishersByIdsAsync(pubIds);
    }
    public async Task<List<Genre>> GetAllGenresAsync(List<int> genIds)
    {
        return await genreRepository.GetGenresByIdsAsync(genIds);
    }
    public async Task<List<User>> GetAllUsersAsync(List<int> userIds)
    {
        return await userRepository.GetUsersByIdsAsync(userIds);
    }
    public async Task AddGameAsync(AddGameRequest payload)
    {
        if(payload == null) 
            throw new ArgumentNullException(nameof(payload));

        var newGame = payload.ToEntity();
        newGame.Developers = await GetAllDevsAsync(payload.DeveloperIds);
        newGame.Publishers = await GetAllPublishersAsync(payload.PublisherIds);
        newGame.Genres = await GetAllGenresAsync(payload.GenreIds);
        newGame.Users = await GetAllUsersAsync(payload.UserIds);
        await gameRepository.AddGameAsync(newGame);
    }

    public async Task UpdateGameAsync(int id, UpdateGameRequest payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var updatedEntity = payload.ToEntity();
        

        if (payload.DeveloperIds != null)
        {
            updatedEntity.Developers = await GetAllDevsAsync(payload.DeveloperIds);
        }

        if (payload.PublisherIds != null)
        {
            updatedEntity.Publishers = await GetAllPublishersAsync(payload.PublisherIds);
        }

        if (payload.GenreIds != null)
        {
            updatedEntity.Genres = await GetAllGenresAsync(payload.GenreIds);
        }

        if (payload.UserIds != null)
        {
            updatedEntity.Users = await GetAllUsersAsync(payload.UserIds);
        }

        await gameRepository.UpdateGameAsync(id, updatedEntity);
    }

    public async Task<IEnumerable<Game>> GetGamesAsync()
    {
        return await gameRepository.GetGamesAsync();
    }

    public async Task<IEnumerable<GetGamesResponse>> GetGamesWithInfoAsync()
    {
        var games = await gameRepository.GetGamesWithInfoAsync();
        return games.Select(g => g.ToResponseDto());
    }

    public async Task<(IEnumerable<GetGamesResponse> Games, int TotalCount)> GetGamesPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (result, total) = await gameRepository.GetGamesAsync(pageNumber, pageSize);
        return (result.Select(g => g.ToResponseDto()).ToList(), total);
    }

    public async Task<GetGamesResponse?> GetGameFromIdAsync(int id)
    {
        var result = await gameRepository.GetGameByIdAsync(id);
        return result?.ToResponseDto();
    }

    public async Task SoftDeleteGameAsync(int id)
    {
        await gameRepository.SoftDeleteGameAsync(id);
    }
}