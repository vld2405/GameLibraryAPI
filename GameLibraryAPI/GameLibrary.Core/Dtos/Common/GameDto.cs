using GameLibrary.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Common
{
    public class GameDto
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        public List<UserDto> Users { get; set; }
        public List<GenreDto> Types { get; set; }
        public List<DeveloperDto> Developers { get; set; }
        public List<PublisherDto> Publishers { get; set; }

    }
}
