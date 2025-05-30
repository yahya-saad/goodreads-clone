using Goodreads.API.Common;
using Goodreads.Application.Books.Commands.AddGenresToBook;
using Goodreads.Application.Books.Commands.CreateBook;
using Goodreads.Application.Books.Commands.DeleteBook.DeleteBookCommand;
using Goodreads.Application.Books.Commands.RemoveGenreFromBook;
using Goodreads.Application.Books.Commands.UpdateBook;
using Goodreads.Application.Books.Queries.GetAllBooks;
using Goodreads.Application.Books.Queries.GetBookById;
using Goodreads.Application.Books.Queries.GetBooksByGenre;
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
public class BooksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all books")]
    [ProducesResponseType(typeof(PagedResult<BookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllBooks([FromQuery] QueryParameters parameters)
    {
        var result = await mediator.Send(new GetAllBooksQuery(parameters));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [EndpointSummary("Get book by ID")]
    [ProducesResponseType(typeof(ApiResponse<BookDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookById(string id)
    {
        var result = await mediator.Send(new GetBookByIdQuery(id));

        return result.Match(
             book => Ok(ApiResponse<BookDetailDto>.Success(book)),
             failure => CustomResults.Problem(failure)
        );
    }

    [HttpPost]
    [Authorize]
    [EndpointSummary("Create a new book")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromForm] CreateBookCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            id => CreatedAtAction(nameof(GetBookById), new { id }, ApiResponse.Success("Book created successfully")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPut]
    [Authorize]
    [EndpointSummary("Update a book")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    [EndpointSummary("Delete a book")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(string id)
    {
        var result = await mediator.Send(new DeleteBookCommand(id));
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost("{bookId}/genres")]
    [Authorize]
    [EndpointSummary("Add genres to a book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddGenresToBook(string bookId, [FromBody] List<string> GenreIds)
    {
        var result = await mediator.Send(new AddGenresToBookCommand(bookId, GenreIds));
        return result.Match(
            () => Ok(),
            failure => CustomResults.Problem(failure));
    }

    [HttpDelete("{bookId}/genres/{genreId}")]
    [Authorize]
    [EndpointSummary("Remove a genre from a book")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveGenreFromBook(string bookId, string genreId)
    {
        var result = await mediator.Send(new RemoveGenreFromBookCommand(bookId, genreId));
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }


    [HttpGet("by-genre")]
    [EndpointSummary("Get books by genre")]
    [ProducesResponseType(typeof(PagedResult<BookDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetBooksByGenre([FromQuery] QueryParameters parameters)
    {
        var result = await mediator.Send(new GetBooksByGenreQuery(parameters));
        return Ok(result);
    }

}
