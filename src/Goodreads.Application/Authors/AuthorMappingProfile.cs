using Goodreads.Application.Authors.Commands.CreateAuthor;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Authors;
internal class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorCommand, Author>();
    }
}
