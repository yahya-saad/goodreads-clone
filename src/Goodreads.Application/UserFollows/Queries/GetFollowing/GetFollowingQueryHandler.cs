using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace Goodreads.Application.UserFollows.Queries.GetFollowing;

internal class GetFollowingQueryHandler : IRequestHandler<GetFollowingQuery, Result<PagedResult<UserDto>>>
{
    private readonly IUserFollowRepository _userFollowRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IUserContext _context;

    public GetFollowingQueryHandler(
        IUserFollowRepository userFollowRepository,
        UserManager<User> userManager,
        IMapper mapper,
        IUserContext context)
    {
        _userFollowRepository = userFollowRepository;
        _userManager = userManager;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<PagedResult<UserDto>>> Handle(GetFollowingQuery request, CancellationToken cancellationToken)
    {
        var userId = _context.UserId;
        if (userId == null)
            return Result<PagedResult<UserDto>>.Fail(AuthErrors.Unauthorized);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result<PagedResult<UserDto>>.Fail(FollowErrors.UserNotFound(userId));

        var following = await _userFollowRepository.GetFollowingAsync(userId, request.PageNumber, request.PageSize);
        var totalCount = await _userFollowRepository.GetFollowingCountAsync(userId);

        var dtoList = _mapper.Map<List<UserDto>>(following);

        var pagedResult = PagedResult<UserDto>.Create(
            dtoList,
            request.PageNumber ?? 1,
            request.PageSize ?? totalCount,
            totalCount
        );

        return Result<PagedResult<UserDto>>.Ok(pagedResult);
    }
}
