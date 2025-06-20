using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
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

    public async Task<(IEnumerable<GetGenreResponse> Genres, int TotalCount)> GetGenresPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (result, total) = await genreRepository.GetGenresAsync(pageNumber, pageSize);
        return (result.Select(g => g.ToResponseDto()).ToList(), total);
    }

    public async Task<Genre?> GetGenreFromIdAsync(int id)
    {
        return await genreRepository.GetGenresAsync(id);
    }

    public async Task SoftDeleteGenreAsync(int id)
    {
        await genreRepository.SoftDeleteGenreAsync(id);
    }
}