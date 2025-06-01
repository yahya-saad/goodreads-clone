using Goodreads.Application.Authors.Commands.CreateAuthor;

namespace Goodreads.Application.Authors;
internal class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorCommand, Author>();
    }
}
