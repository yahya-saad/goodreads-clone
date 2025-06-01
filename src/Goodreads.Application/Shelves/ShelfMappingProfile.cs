namespace Goodreads.Application.Shelves;
public class ShelfMappingProfile : Profile
{
    public ShelfMappingProfile()
    {
        CreateMap<Shelf, ShelfDto>()
            .ForMember(dest => dest.BookCount, opt => opt.MapFrom(src => src.BookShelves.Count))
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.BookShelves.Select(bs => bs.Book)));
    }
}
