using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Responses
{
    public class GetGamesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<string> GenreNames { get; set; }
        public List<string> DeveloperNames { get; set; }
        public List<string> PublisherNames { get; set; }
        public List<string> OwnedByUsernames { get; set; }
    }
}
