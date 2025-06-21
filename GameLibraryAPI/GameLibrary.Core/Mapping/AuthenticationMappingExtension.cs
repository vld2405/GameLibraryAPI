using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Database.Entities;

namespace GameLibrary.Core.Mapping
{
    public static class AuthenticationMappingExtension
    {
        public static User ToEntity(this RegisterRequest registerDto)
        {
            User user = new User();
            user.Username = registerDto.Username;
            user.Email = registerDto.Email;

            return user;
        }
    }
}
