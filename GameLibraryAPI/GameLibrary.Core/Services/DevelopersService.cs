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
        newDev.CreatedAt = DateTime.UtcNow;

        devsRepository.Insert(newDev);
        await devsRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Developer>> GetDevsAsync()
    {
        return await devsRepository.GetDevelopersAsync();
    }
}
