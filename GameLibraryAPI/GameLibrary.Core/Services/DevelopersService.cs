using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;

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

    public async Task<IEnumerable<Developer>> GetDevsAsync()
    {
        return await devsRepository.GetDevelopersAsync();
    }

    public async Task<(IEnumerable<GetDeveloperResponse> Developers, int TotalCount)> GetDevsPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (result, total) = await devsRepository.GetDevelopersAsync(pageNumber, pageSize);
        return (result.Select(d => d.ToResponseDto()).ToList(), total);
    }

    public async Task<Developer?> GetDeveloperFromIdAsync(int id)
    {
        return await devsRepository.GetDeveloperByIdAsync(id);
    }

    public async Task SoftDeleteDeveloperAsync(int id)
    {
        await devsRepository.SoftDeleteDevAsync(id);
    }
}
