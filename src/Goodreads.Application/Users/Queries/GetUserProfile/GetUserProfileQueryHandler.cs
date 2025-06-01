namespace Goodreads.Application.Users.Queries.GetUserProfile;
internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Result<UserProfileDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<GetUserProfileQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;
    public GetUserProfileQueryHandler(
        UserManager<User> userManager,
        ILogger<GetUserProfileQueryHandler> logger,
        IMapper mapper,
        IUserContext userContext)
    {
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
        _userContext = userContext;
    }
    public async Task<Result<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result<UserProfileDto>.Fail(AuthErrors.Unauthorized);

        _logger.LogInformation("Getting User profile for {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return Result<UserProfileDto>.Fail(UserErrors.NotFound(userId));
        }

        var dto = _mapper.Map<UserProfileDto>(user);
        return Result<UserProfileDto>.Ok(dto);
    }
}
