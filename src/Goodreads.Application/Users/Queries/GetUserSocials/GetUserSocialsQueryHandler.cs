using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.DTOs;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.Users.Queries.GetUserSocials;
internal class GetUserSocialsQueryHandler : IRequestHandler<GetUserSocialsQuery, Result<SocialDto>>
{
    private readonly IUserContext _userContext;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<GetUserSocialsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetUserSocialsQueryHandler(
        IUserContext userContext,
        UserManager<User> userManager,
        ILogger<GetUserSocialsQueryHandler> logger,
        IMapper mapper)
    {
        _userContext = userContext;
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<SocialDto>> Handle(GetUserSocialsQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result<SocialDto>.Fail(AuthErrors.Unauthenticated());

        _logger.LogInformation("Fetching socials for user: {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result<SocialDto>.Fail(UserErrors.NotFound(userId));
        }

        var socials = _mapper.Map<SocialDto>(user.Social);

        return Result<SocialDto>.Ok(socials);
    }
}

