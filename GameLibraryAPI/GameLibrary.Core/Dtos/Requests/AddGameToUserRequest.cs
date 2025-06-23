using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Requests
{
    public class AddGameToUserRequest
    {
        public List<int>? GamesIds { get; set; }
    }
}
