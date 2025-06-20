using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;

namespace GameLibrary.Core.Services;

// TODO: GenresService
public class GenresService(GenreRepository genreRepository)
{
    public async Task AddGenreAsync(AddGenreRequest payload)
    {
        if(payload == null)
            throw new ArgumentNullException(nameof(payload));

        var newGenre = payload.ToEntity();

        await genreRepository.AddGenreAsync(newGenre);
    }

    public async Task<IEnumerable<Genre>> GetGenresAsync()
    {
        return await genreRepository.GetGenresAsync();
    }
}