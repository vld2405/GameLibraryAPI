using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;

namespace GameLibrary.Core.Services;

public class GenresService(GenreRepository genreRepository)
{
    public async Task AddGenreAsync(AddGenreRequest payload)
    {
        if(payload == null)
            throw new ArgumentNullException(nameof(payload));

        var newGenre = payload.ToEntity();

        await genreRepository.AddGenreAsync(newGenre);
    }

    public async Task UpdateGenreAsync(int id, UpdateGenreRequest payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var updatedEntity = payload.ToEntity();

        await genreRepository.UpdateGenreAsync(id, updatedEntity);
    }

    public async Task<IEnumerable<GetGenreResponse>> GetGenresAsync()
    {
        var result = await genreRepository.GetGenresAsync();
        return result.Select(g => g.ToResponseDto()).ToList();
    }

    public async Task<(IEnumerable<GetGenreResponse> Genres, int TotalCount)> GetGenresPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (result, total) = await genreRepository.GetGenresAsync(pageNumber, pageSize);
        return (result.Select(g => g.ToResponseDto()).ToList(), total);
    }

    public async Task<IEnumerable<GetGenreResponse>> GetGenresFilteredAsync(string? name = null, string? sortOrder = "asc")
    {
        var result = await genreRepository.GetGenresFilteredAsync(name, sortOrder);
        return result.Select(g => g.ToResponseDto()).ToList();
    }

    public async Task<GetGenreResponse?> GetGenreFromIdAsync(int id)
    {
        var result = await genreRepository.GetGenreByIdAsync(id);
        return result?.ToResponseDto();
    }

    public async Task SoftDeleteGenreAsync(int id)
    {
        await genreRepository.SoftDeleteGenreAsync(id);
    }
}