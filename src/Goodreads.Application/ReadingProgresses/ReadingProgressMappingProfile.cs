using Goodreads.Application.DTOs;

namespace Goodreads.Application.ReadingProgresses;
public class ReadingProgressMappingProfile : Profile
{
    public ReadingProgressMappingProfile()
    {
        CreateMap<ReadingProgress, ReadingProgressDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.Book.PageCount))
            .ForMember(dest => dest.ProgressPercent, opt => opt.MapFrom(src =>
                src.Book.PageCount == 0
                    ? 0
                    : Math.Round((double)src.CurrentPage / src.Book.PageCount * 100, 2)));
    }


}
