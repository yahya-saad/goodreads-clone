namespace Goodreads.Application.AuthorClaims;
public class AuthorClaimRequestProfile : Profile
{
    public AuthorClaimRequestProfile()
    {
        CreateMap<AuthorClaimRequest, AuthorClaimRequestDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
    }
}
