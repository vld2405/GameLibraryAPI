using Azure.Core;
using GameLibrary.Core.Dtos.Common;
using GameLibrary.Core.Dtos.Requests;
using GameLibrary.Core.Dtos.Responses;
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

        public static Game ToEntity(this UpdateGameRequest dto)
        {
            var game = new Game();
            if (!string.IsNullOrEmpty(dto.Name))
                game.Name = dto.Name!;
            if (!string.IsNullOrEmpty(dto.Description))
                game.Description = dto.Description!;
            if (dto.ReleaseDate.HasValue)
                game.ReleaseDate = dto.ReleaseDate.Value;
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
        public static GetGamesResponse ToResponseDto(this Game game)
        {
            return new GetGamesResponse
            {
                Id = game.Id,
                Name = game.Name,
                ReleaseDate = game.ReleaseDate,
                Description = game.Description,
                CreatedAt = game.CreatedAt,
                ModifiedAt = game.ModifiedAt,
                DeletedAt = game.DeletedAt,
                GenreNames = game.Genres?.Select(g => g.Name).ToList() ?? new List<string>(),
                DeveloperNames = game.Developers?.Select(d => d.Name).ToList() ?? new List<string>(),
                PublisherNames = game.Publishers?.Select(p => p.Name).ToList() ?? new List<string>()
            };
        }
    }
}
