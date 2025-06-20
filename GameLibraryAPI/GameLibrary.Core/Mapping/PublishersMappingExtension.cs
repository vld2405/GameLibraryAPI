using GameLibrary.Core.Dtos.Common;
using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Mapping
{
    public static class PublishersMappingExtension
    {
        public static Publisher ToEntity(this AddPublisherRequest publisherDto)
        {
            Publisher publisher = new Publisher();
            publisher.Name = publisherDto.Name;

            return publisher;
        }

        public static PublisherDto ToDto(Publisher publisher)
        {
            return new PublisherDto
            {
                Name = publisher.Name
            };
        }

        public static GetPublisherResponse ToResponseDto(this Publisher publisher)
        {
            return new GetPublisherResponse
            {
                Name = publisher.Name,
                GamesNames = publisher.Games?.Select(g => g.Name).ToList() ?? new List<string>(),
                CreatedAt = publisher.CreatedAt,
                ModifiedAt = publisher.ModifiedAt,
                DeletedAt = publisher.DeletedAt
            };
        }
    }
}
