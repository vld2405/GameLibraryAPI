using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Mapping;
using GameLibrary.Database.Entities;
using GameLibrary.Database.Repositories;

namespace GameLibrary.Core.Services;

// TODO: PublisherService
public class PublisherService(PublisherRepository publisherRepository)
{
    public async Task AddPublisherAsync(AddPublisherRequest payload)
    {
        if(payload == null)
            throw new ArgumentNullException(nameof(payload));

        var newPublisher = payload.ToEntity();
        newPublisher.CreatedAt = DateTime.UtcNow;

        publisherRepository.Insert(newPublisher);
        await publisherRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Publisher>> GetPublishersAsync()
    {
        return await publisherRepository.GetPublishersAsync();
    }
}
