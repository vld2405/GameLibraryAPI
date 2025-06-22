using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;
using System.ComponentModel;

namespace GameLibrary.Core.Services;

public class GamesService(GameRepository gameRepository)
{
    public async Task AddGameAsync(AddGameRequest payload)
    {
        if(payload == null) 
            throw new ArgumentNullException(nameof(payload));

        var newGame = payload.ToEntity();

        await gameRepository.AddGameAsync(newGame, payload.DeveloperIds, payload.PublisherIds, payload.GenreIds, payload.UserIds);
    }

    public async Task UpdateGameAsync(int id, UpdateGameRequest payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var updatedEntity = payload.ToEntity();

        await gameRepository.UpdateGameAsync(id, updatedEntity, payload.DeveloperIds, payload.PublisherIds, payload.GenreIds, payload.UserIds);
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