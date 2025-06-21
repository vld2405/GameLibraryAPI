using GameLibrary.Core.Dtos.Common;
using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
using GameLibrary.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Mapping
{
    public static class DevelopersMappingExtension
    {
        public static Developer ToEntity(this AddDeveloperRequest developerDto)
        {
            Developer developer = new Developer();
            developer.Name = developerDto.Name;

            return developer;
        }

        public static Developer ToEntity(this UpdateDeveloperRequest dto)
        {
            var dev = new Developer();
            if (!string.IsNullOrEmpty(dto.Name))
                dev.Name = dto.Name!;
            return dev;
        }


        public static GetDeveloperResponse ToResponseDto(this Developer developer)
        {
            return new GetDeveloperResponse
            {
                Id = developer.Id,
                Name = developer.Name,
                GamesNames = developer.Games?.Select(g => g.Name).ToList() ?? new List<string>(),
                CreatedAt = developer.CreatedAt,
                ModifiedAt = developer.ModifiedAt,
                DeletedAt = developer.DeletedAt
            };
        }
    }
}
