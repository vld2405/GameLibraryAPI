using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
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

        await publisherRepository.AddPublisherAsync(newPublisher);
    }

    public async Task<IEnumerable<Publisher>> GetPublishersAsync()
    {
        return await publisherRepository.GetPublishersAsync();
    }

    public async Task<(IEnumerable<GetPublisherResponse> Publishers, int TotalCount)> GetPublishersPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (result, total) = await publisherRepository.GetPublishersAsync(pageNumber, pageSize);
        return (result.Select(p => p.ToResponseDto()).ToList(), total);
    }

    public async Task<Publisher?> GetPublisherFromIdAsync(int id)
    {
        return await publisherRepository.GetPublisherByIdAsync(id);
    }

    public async Task SoftDeletePublisherAsync(int id)
    {
        await publisherRepository.SoftDeletePublisherAsync(id);
    }
}
