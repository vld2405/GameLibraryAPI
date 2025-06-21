using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;
using System.ComponentModel;

namespace GameLibrary.Core.Services;

// TODO: GamesService
public class GamesService(GameRepository gameRepository)
{
    public async Task AddGameAsync(AddGameRequest payload)
    {
        if(payload == null) 
            throw new ArgumentNullException(nameof(payload));

        var newGame = payload.ToEntity();

        await gameRepository.AddGameAsync(newGame, payload.DeveloperIds, payload.PublisherIds, payload.GenreIds);
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

    public async Task<Game?> GetGameFromIdAsync(int id)
    {
        return await gameRepository.GetGamesByIdAsync(id);
    }

    public async Task SoftDeleteGameAsync(int id)
    {
        await gameRepository.SoftDeleteGameAsync(id);
    }
}