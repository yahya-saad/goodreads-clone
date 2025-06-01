using Goodreads.Application.Quotes.Commands.CreateQuote;

namespace Goodreads.Application.Quotes;
public class QuoteMappingProfile : Profile
{
    public QuoteMappingProfile()
    {
        CreateMap<CreateQuoteCommand, Quote>();
        CreateMap<Quote, QuoteDto>()
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.LikesCount));

    }
}
