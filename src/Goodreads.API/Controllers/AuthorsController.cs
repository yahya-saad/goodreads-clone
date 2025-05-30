using Goodreads.API.Common;
using Goodreads.Application.Authors.Commands.CreateAuthor;
using Goodreads.Application.Authors.Commands.DeleteAuthor;
using Goodreads.Application.Authors.Commands.UpdateAuthor;
using Goodreads.Application.Authors.Queries.GetAllAuthors;
using Goodreads.Application.Authors.Queries.GetAuthorById;
using Goodreads.Application.Books.Queries.GetBooksByAuthor;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all authors")]
    [ProducesResponseType(typeof(PagedResult<AuthorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthors([FromQuery] QueryParameters parameters)
    {
        var result = await mediator.Send(new GetAllAuthorsQuery(parameters));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Get author by ID")]
    [ProducesResponseType(typeof(ApiResponse<AuthorDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthorById(string id)
    {
        var result = await mediator.Send(new GetAuthorByIdQuery(id));
        return result.Match(
            author => Ok(ApiResponse<AuthorDto>.Success(author)),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Create a new author")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAuthor([FromForm] CreateAuthorCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            id => CreatedAtAction(nameof(GetAuthorById), new { id }, ApiResponse.Success("Author created successfully")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPut]
    [Authorize]
    [EndpointSummary("Update an author")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAuthor([FromForm] UpdateAuthorCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Delete an author")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAuthor(string id)
    {
        var result = await mediator.Send(new DeleteAuthorCommand(id));
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpGet("{authorId}/books")]
    [EndpointSummary("Get books by author ID")]
    [ProducesResponseType(typeof(PagedResult<BookDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBooksByAuthor(string authorId, [FromQuery] QueryParameters parameters)
    {
        var result = await mediator.Send(new GetBooksByAuthorQuery(authorId, parameters));
        return Ok(result);
    }
}
