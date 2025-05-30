using Goodreads.API.Common;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.Shelves.Commands.AddBookToShelf;
using Goodreads.Application.Shelves.Commands.CreateShelf;
using Goodreads.Application.Shelves.Commands.DeleteShelf;
using Goodreads.Application.Shelves.Commands.RemoveBookFromShelf;
using Goodreads.Application.Shelves.Commands.UpdateShelf;
using Goodreads.Application.Shelves.Queries.GetShelfById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShelvesController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    [EndpointSummary("Get shelf by ID")]
    [ProducesResponseType(typeof(ApiResponse<ShelfDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetShelfById(string id)
    {
        var result = await mediator.Send(new GetShelfByIdQuery(id));
        return result.Match(
            shelf => Ok(ApiResponse<ShelfDto>.Success(shelf)),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost]
    [Authorize]
    [EndpointSummary("Create a new shelf")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateShelf([FromBody] CreateShelfCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            id => CreatedAtAction(nameof(GetShelfById), new { id }, ApiResponse.Success("Shelf created successfully")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPut]
    [Authorize]
    [EndpointSummary("Update a shelf")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateShelf([FromBody] UpdateShelfCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpDelete("{id}")]
    [Authorize]
    [EndpointSummary("Delete a shelf")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteShelf(string id)
    {
        var result = await mediator.Send(new DeleteShelfCommand(id));
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }


    [HttpPost("{shelfId}/books/{bookId}")]
    [Authorize]
    [EndpointSummary("Add book to shelf")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> AddBookToShelf(string shelfId, string bookId)
    {
        var result = await mediator.Send(new AddBookToShelfCommand(shelfId, bookId));
        return result.Match(
            () => Ok(),
            failure => CustomResults.Problem(failure));
    }

    [HttpDelete("{shelfId}/books/{bookId}")]
    [Authorize]
    [EndpointSummary("Delete book from shelf")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveBookFromShelf(string shelfId, string bookId)
    {
        var result = await mediator.Send(new RemoveBookFromShelfCommand(shelfId, bookId));
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }
}
