using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Requests
{
    public class UpdateGameRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public List<int>? DeveloperIds { get; set; }
        public List<int>? PublisherIds { get; set; }
        public List<int>? GenreIds { get; set; }
        public List<int>? UserIds { get; set; }
    }
}
