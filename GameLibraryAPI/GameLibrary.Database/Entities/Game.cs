using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        public List<User> Users { get; set; }
        public List<Genre> Types { get; set; }
        public List<Developer> Developers { get; set; }
        public List<Publisher> Publishers { get; set; }

    }
}
