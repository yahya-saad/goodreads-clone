using Goodreads.API.Common;
using Goodreads.Application.AuthorClaims.Commands.RequestAuthorClaim;
using Goodreads.Application.AuthorClaims.Commands.ReviewAuthorClaim;
using Goodreads.Application.AuthorClaims.Queries.GetAllAuthorClaimRequests;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/author-claims")]
public class AuthorClaimRequestsController(IMediator mediator) : ControllerBase
{
    [HttpPost("request")]
    [Authorize(Roles = Roles.User)]
    [EndpointSummary("Request claim for an author")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RequestClaimAuthor([FromBody] RequestAuthorClaimCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match(
             () => Ok(ApiResponse.Success("Claim request submitted successfully.")),
             failure => CustomResults.Problem(failure)
         );
    }

    [HttpPut("review")]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Review an author claim request")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReviewClaimRequest([FromBody] ReviewAuthorClaimCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => Ok(ApiResponse.Success("Claim request reviewed.")),
            failure => CustomResults.Problem(failure)
        );
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Get all author claim requests")]
    [ProducesResponseType(typeof(PagedResult<AuthorClaimRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllAuthorClaimRequestsQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}