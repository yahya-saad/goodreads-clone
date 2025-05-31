using Goodreads.API.Common;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.ReadingProgresses.Commands.UpdateReadingProgress;
using Goodreads.Application.ReadingProgresses.Queries.GetReadingProgresses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReadingProgressController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReadingProgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost]
    [EndpointSummary("Update reading progress for a book")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateReadingProgress([FromBody] UpdateReadingProgressCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match(
            () => Ok(ApiResponse.Success("Reading progress updated successfully")),
            failure => CustomResults.Problem(failure)
        );
    }

    [Authorize]
    [HttpGet]
    [EndpointSummary("Get reading progresses for the current user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<ReadingProgressDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> GetReadingProgresses([FromQuery] QueryParameters parameters)
    {
        var query = new GetReadingProgressesQuery(parameters);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
