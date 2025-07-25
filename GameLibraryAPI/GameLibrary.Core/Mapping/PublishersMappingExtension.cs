﻿using GameLibrary.Core.Dtos.Common;
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

        public static Publisher ToEntity(this UpdatePublisherRequest dto)
        {
            var publisher = new Publisher();
            if (!string.IsNullOrEmpty(dto.Name))
                publisher.Name = dto.Name!;
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
                Id = publisher.Id,
                Name = publisher.Name,
                GamesNames = publisher.Games?.Where(p => p.DeletedAt == null).Select(g => g.Name).ToList() ?? new List<string>(),
                CreatedAt = publisher.CreatedAt,
                ModifiedAt = publisher.ModifiedAt,
                DeletedAt = publisher.DeletedAt
            };
        }
    }
}
