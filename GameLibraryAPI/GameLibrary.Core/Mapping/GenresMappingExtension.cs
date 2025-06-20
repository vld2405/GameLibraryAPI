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
    public static class GenresMappingExtension
    {
        public static Genre ToEntity(this AddGenreRequest genreDto)
        {
            Genre genre = new Genre();
            genre.Name = genreDto.Name;

            return genre;
        }

        public static GenreDto ToDto(Genre genre)
        {
            return new GenreDto
            {
                Name = genre.Name
            };
        }
        public static GetGenreResponse ToResponseDto(this Genre genre)
        {
            return new GetGenreResponse
            {
                Name = genre.Name,
                GamesNames = genre.Games?.Select(g => g.Name).ToList() ?? new List<string>(),
                CreatedAt = genre.CreatedAt,
                ModifiedAt = genre.ModifiedAt,
                DeletedAt = genre.DeletedAt
            };
        }
    }
}
