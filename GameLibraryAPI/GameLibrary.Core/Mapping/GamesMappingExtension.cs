using Azure.Core;
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
    public static class GamesMappingExtension
    {
        public static Game ToEntity(this AddGameRequest gameDto)
        {
            Game game = new Game();
            game.Name = gameDto.Name;
            game.Description = gameDto.Description;

            game.ReleaseDate = gameDto.ReleaseDate;

            return game;
        }

        public static GameDto ToDto(Game game)
        {
            return new GameDto
            {
                Name = game.Name,
                Description = game.Description,
                ReleaseDate = game.ReleaseDate,
            };
        }
    }
}
