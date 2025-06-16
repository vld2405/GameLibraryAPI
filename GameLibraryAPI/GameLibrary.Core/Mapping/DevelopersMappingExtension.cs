using GameLibrary.Core.Dtos.Common;
using GameLibrary.Core.Dtos.Requests;
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

        public static DeveloperDto ToDto(Developer developer)
        {
            return new DeveloperDto
            {
                Name = developer.Name
            };
        }
    }
}
