using GameLibrary.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Common
{
    public class PublisherDto
    {
        public string Name { get; set; }

        public List<GameDto> Games { get; set; }
    }
}
