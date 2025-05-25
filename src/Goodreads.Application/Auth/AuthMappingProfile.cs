using Goodreads.Application.Auth.Commands.RegisterUser;

namespace Goodreads.Application.Auth;
internal class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<RegisterUserCommand, User>();

    }
}
