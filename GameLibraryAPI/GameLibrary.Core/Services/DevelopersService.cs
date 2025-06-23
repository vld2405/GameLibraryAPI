using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Core.Services;

public class DevelopersService(DeveloperRepository devsRepository)
{
    public async Task AddDevAsync(AddDeveloperRequest payload)
    {
        if(payload == null)
            throw new ArgumentNullException(nameof(payload));

        var newDev = payload.ToEntity();

        await devsRepository.AddDevAsync(newDev);
    }
    public async Task UpdateDeveloperAsync(int id, UpdateDeveloperRequest payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var updatedEntity = payload.ToEntity();

        await devsRepository.UpdateDeveloperAsync(id, updatedEntity);
    }

    public async Task<IEnumerable<GetDeveloperResponse>> GetDevsAsync()
    {
        var result = await devsRepository.GetDevelopersAsync();
        return result.Select(d => d.ToResponseDto()).ToList();
    }

    public async Task<(IEnumerable<GetDeveloperResponse> Developers, int TotalCount)> GetDevsPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (result, total) = await devsRepository.GetDevelopersAsync(pageNumber, pageSize);
        return (result.Select(d => d.ToResponseDto()).ToList(), total);
    }

    public async Task<GetDeveloperResponse?> GetDeveloperFromIdAsync(int id)
    {
        var result = await devsRepository.GetDeveloperByIdAsync(id);
        return result?.ToResponseDto();
    }
    
    public async Task<IEnumerable<GetDeveloperResponse>> GetDeveloperFilteredAsync(string? name = null, string? sortOrder = "asc")
    {
        var result = await devsRepository.GetDevelopersFilteredAsync(name, sortOrder);
        return result.Select(d => d.ToResponseDto()).ToList();
    }
    
    public async Task<(IEnumerable<GetDeveloperResponse> Items, int TotalCount)> GetDevelopersPaginatedAndFilteredAsync(int pageNumber, int pageSize, string? name = null, string? sortOrder = "asc")
    {
        var (result, total) = await devsRepository.GetDevelopersPaginatedAndFilteredAsync(pageNumber, pageSize, name, sortOrder);
        return (result.Select(d => d.ToResponseDto()).ToList(), total);
    }

    public async Task SoftDeleteDeveloperAsync(int id)
    {
        await devsRepository.SoftDeleteDevAsync(id);
    }
}
