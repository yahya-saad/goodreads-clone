using Goodreads.API.Common;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.UserYearChallenges.Commands.UpdateUserYearChallenge;
using Goodreads.Application.UserYearChallenges.Queries.GetAllUserYearChallenges;
using Goodreads.Application.UserYearChallenges.Queries.GetUserYearChallenge;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserYearChallengeController(IMediator mediator) : ControllerBase
{

    [HttpGet("{year:int}")]
    [Authorize]
    [EndpointSummary("Get challenge details for a specific year")]
    [ProducesResponseType(typeof(UserYearChallengeDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetChallengeDetails(int year, [FromQuery] string userId)
    {
        var result = await mediator.Send(new GetUserYearChallengeQuery(userId, year));
        return result.Match(
            challenge => Ok(ApiResponse<UserYearChallengeDetailsDto>.Success(challenge)),
            failure => CustomResults.Problem(failure)
        );
    }


    [HttpGet]
    [Authorize]
    [EndpointSummary("Get all user year challenges")]
    [ProducesResponseType(typeof(PagedResult<UserYearChallengeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllChallenges(
        [FromQuery] QueryParameters parameters,
        [FromQuery] int? year,
        [FromQuery] string userId)
    {
        var result = await mediator.Send(new GetAllUserYearChallengesQuery(userId, parameters, year));
        return Ok(result);
    }


    [HttpPost("upsert")]
    [Authorize]
    [EndpointSummary("Upsert a user year challenge")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpsertChallenge([FromBody] UpsertUserYearChallengeCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => Ok(),
            failure => CustomResults.Problem(failure)
        );
    }


}
