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
    public async Task UpdatePublisherAsync(int id, AddPublisherRequest payload)
    {
        if (payload == null)
            throw new ArgumentNullException(nameof(payload));

        var updatedEntity = payload.ToEntity();

        await publisherRepository.UpdatePublisherAsync(id, updatedEntity);
    }

    public async Task<IEnumerable<GetPublisherResponse>> GetPublishersAsync()
    {
        var result = await publisherRepository.GetPublishersAsync();
        return result.Select(p => p.ToResponseDto()).ToList();
    }

    public async Task<(IEnumerable<GetPublisherResponse> Publishers, int TotalCount)> GetPublishersPaginatedAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (result, total) = await publisherRepository.GetPublishersAsync(pageNumber, pageSize);
        return (result.Select(p => p.ToResponseDto()).ToList(), total);
    }

    public async Task<IEnumerable<GetPublisherResponse>> GetPublishersFilteredAsync(string? name = null, string? sortOrder = "asc")
    {
        var result = await publisherRepository.GetPublishersFilteredAsync(name, sortOrder);
        return result.Select(p => p.ToResponseDto()).ToList();
    }

    public async Task<GetPublisherResponse?> GetPublisherFromIdAsync(int id)
    {
        var result = await publisherRepository.GetPublisherByIdAsync(id);
        return result?.ToResponseDto();
    }

    public async Task SoftDeletePublisherAsync(int id)
    {
        await publisherRepository.SoftDeletePublisherAsync(id);
    }
}
