using GameLibrary.Core.Dtos.Requests;
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

    public async Task<(IEnumerable<Developer> developers, int totalCount)> GetDevsPaginatedAsync(int? pageNumber = null, int? pageSize = null)
    {
        return await devsRepository.GetDevelopersAsync(pageNumber, pageSize);
    }
}
