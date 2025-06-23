using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Dtos.Responses
{
    public class GetUsersResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public List<string> GamesNames { get; set; }
    }
}
