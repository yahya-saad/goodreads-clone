using Goodreads.Application.Auth.Commands;

namespace Goodreads.Application.Auth;
internal class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<RegisterUserCommand, User>();

    }
}
