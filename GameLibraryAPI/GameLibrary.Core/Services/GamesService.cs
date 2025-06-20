using GameLibrary.Core.Dtos.Requests;
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

        gameRepository.Insert(newGame);
        await gameRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game>> GetGamesAsync()
    {
        return await gameRepository.GetGamesAsync();
    }
}