using Goodreads.API.Common;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.UserFollows.Commands.FollowUser;
using Goodreads.Application.UserFollows.Commands.UnfollowUser;
using Goodreads.Application.UserFollows.Queries.GetFollowers;
using Goodreads.Application.UserFollows.Queries.GetFollowing;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/User")]
public class UserFollowController(IMediator mediator) : ControllerBase
{
    [HttpPost("follow")]
    [Authorize]
    [EndpointSummary("Follow a user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Follow([FromBody] FollowUserCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match(
            () => Ok(ApiResponse.Success("User followed successfully.")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost("unfollow")]
    [Authorize]
    [EndpointSummary("Follow a user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Unfollow([FromBody] UnfollowUserCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match(
            () => Ok(ApiResponse.Success("User unfollowed successfully.")),
            failure => CustomResults.Problem(failure));
    }

    [HttpGet("followers")]
    [Authorize]
    [EndpointSummary("Get followers of a user")]
    [ProducesResponseType(typeof(PagedResult<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFollowers(int? pageNumber, int? pageSize)
    {
        var result = await mediator.Send(new GetFollowersQuery(pageNumber, pageSize));

        return result.Match(
            followers => Ok(followers),
            failure => CustomResults.Problem(failure));
    }

    [HttpGet("following")]
    [Authorize]
    [EndpointSummary("Get following of a user")]
    [ProducesResponseType(typeof(PagedResult<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetFollowing(int? pageNumber, int? pageSize)
    {
        var result = await mediator.Send(new GetFollowingQuery(pageNumber, pageSize));

        return result.Match(
          following => Ok(following),
          failure => CustomResults.Problem(failure));
    }

}
