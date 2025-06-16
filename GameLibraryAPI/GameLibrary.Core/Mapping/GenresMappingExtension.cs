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
    }
}
