using Goodreads.Application.DTOs;

namespace Goodreads.Application.Genres;
public class GenreMappingProfile : Profile
{
    public GenreMappingProfile()
    {
        CreateMap<Genre, GenreDto>()
            .ForMember(dest => dest.BookCount,
                opt => opt.MapFrom(src => src.BookGenres != null ? src.BookGenres.Count : 0));
    }
}