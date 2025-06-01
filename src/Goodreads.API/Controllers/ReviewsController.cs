using Goodreads.API.Common;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.Reviews.Commands.CreateBookReview;
using Goodreads.Application.Reviews.Commands.DeleteReview;
using Goodreads.Application.Reviews.Commands.UpdateReview;
using Goodreads.Application.Reviews.Queries.GetReviewById;
using Goodreads.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    [EndpointSummary("Get a book review by ID")]
    [ProducesResponseType(typeof(ApiResponse<BookReviewDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReviewById(string id)
    {
        var result = await mediator.Send(new GetReviewByIdQuery(id));
        return result.Match(
            review => Ok(ApiResponse<BookReviewDto>.Success(review)),
            error => CustomResults.Problem(error)
        );
    }

    [HttpPost]
    [Authorize(Roles = Roles.User)]
    [EndpointSummary("Create a new book review")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBookReview([FromBody] CreateReviewCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            reviewId => CreatedAtAction(nameof(GetReviewById), new { id = reviewId }, ApiResponse<string>.Success(reviewId)),
            error => CustomResults.Problem(error)
        );
    }

    [HttpPut("{reviewId}")]
    [Authorize]
    [EndpointSummary("Update a book review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateReview(string reviewId, [FromBody] UpdateReviewRequest request)
    {
        var result = await mediator.Send(new UpdateReviewCommand(reviewId, request.Rating, request.ReviewText));
        return result.Match(
            () => NoContent(),
            error => CustomResults.Problem(error)
        );

    }

    [HttpDelete("{reviewId}")]
    [Authorize]
    [EndpointSummary("Delete a book review")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteReview(string reviewId)
    {
        var result = await mediator.Send(new DeleteReviewCommand(reviewId));
        return result.Match(
            () => NoContent(),
            error => CustomResults.Problem(error));
    }

}
