using GameLibrary.Core.Dtos.Common;
using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Mapping
{
    public static class UsersMappingExtension
    {
        public static User ToEntity(this AddUserRequest userDto)
        {
            User user = new User();
            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.Password = userDto.Password;

            return user;
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };
        }

    }
}
