using Goodreads.Application.Books.Commands.CreateBook;
using Goodreads.Application.Books.Commands.UpdateBook;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Books;
public class BooksMappingProfile : Profile
{
    public BooksMappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)));

        CreateMap<Book, BookDetailDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BookGenres.Select(bg => bg.Genre)));


        CreateMap<CreateBookCommand, Book>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BookGenres, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore());

        CreateMap<UpdateBookCommand, Book>()
            .ForMember(dest => dest.PageCount, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Genre, BookGenreDto>();
    }
}