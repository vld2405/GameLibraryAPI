using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Entities
{
    public class Publisher : BaseEntity
    {
        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}
