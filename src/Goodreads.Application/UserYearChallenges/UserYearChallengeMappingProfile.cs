using Goodreads.Application.DTOs;

namespace Goodreads.Application.UserYearChallenges;
public class UserYearChallengeMappingProfile : Profile
{
    public UserYearChallengeMappingProfile()
    {
        CreateMap<UserYearChallenge, UserYearChallengeDto>();
        CreateMap<UserYearChallenge, UserYearChallengeDetailsDto>();

        CreateMap<BookShelf, ChallengeBookDto>()
           .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Book.Id))
           .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
           .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.Book.CoverImageUrl))
           .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Book.Author.Name));
    }
}