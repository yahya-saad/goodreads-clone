using Goodreads.API.Common;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.Genres.Commands.CreateGenre;
using Goodreads.Application.Genres.Commands.DeleteGenre;
using Goodreads.Application.Genres.Commands.UpdateGenre;
using Goodreads.Application.Genres.Queries.GetAllGenres;
using Goodreads.Application.Genres.Queries.GetGenreById;
using Goodreads.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all genres")]
    [ProducesResponseType(typeof(PagedResult<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGenres([FromQuery] QueryParameters parameters)
    {
        var result = await mediator.Send(new GetAllGenresQuery(parameters));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Get genre by ID")]
    [ProducesResponseType(typeof(ApiResponse<GenreDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGenreById(string id)
    {
        var result = await mediator.Send(new GetGenreByIdQuery(id));
        return result.Match(
            genre => Ok(ApiResponse<GenreDto>.Success(genre)),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Create a new genre")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGenre([FromBody] CreateGenreCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            id => CreatedAtAction(nameof(GetGenreById), new { id }, ApiResponse.Success("Genre created successfully")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPut]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Update a genre")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGenre([FromBody] UpdateGenreCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Delete a genre")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteGenre(string id)
    {
        var result = await mediator.Send(new DeleteGenreCommand(id));
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }
}
