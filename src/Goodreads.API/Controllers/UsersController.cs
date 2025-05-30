using Goodreads.API.Common;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using Goodreads.Application.Shelves.Queries.GetUserShelves;
using Goodreads.Application.Users.Commands.ChangePassword;
using Goodreads.Application.Users.Commands.DeleteAccount;
using Goodreads.Application.Users.Commands.DeleteProfilePicture;
using Goodreads.Application.Users.Commands.UpdateProfilePicture;
using Goodreads.Application.Users.Commands.UpdateSocials;
using Goodreads.Application.Users.Commands.UpdateUserProfile;
using Goodreads.Application.Users.Queries.GetAllUsers;
using Goodreads.Application.Users.Queries.GetProfileByUsername;
using Goodreads.Application.Users.Queries.GetUserProfile;
using Goodreads.Application.Users.Queries.GetUserSocials;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator, IUserContext userContext) : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    [EndpointSummary("Get current user profile")]
    [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUserProfile()
    {
        var result = await mediator.Send(new GetUserProfileQuery());

        return result.Match(
               profile => Ok(ApiResponse<UserProfileDto>.Success(profile)),
               failure => CustomResults.Problem(failure));
    }

    [HttpGet("me/socials")]
    [Authorize]
    [EndpointSummary("Get current user social links")]
    [ProducesResponseType(typeof(ApiResponse<SocialDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserSocialLinks()
    {
        var result = await mediator.Send(new GetUserSocialsQuery());
        return result.Match(
            socials => Ok(ApiResponse<SocialDto>.Success(socials)),
            failure => CustomResults.Problem(failure));
    }

    [HttpPut("me/socials")]
    [Authorize]
    [EndpointSummary("Update current user social links")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateSocialLinks([FromBody] UpdateSocialsCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpPut("me")]
    [Authorize]
    [EndpointSummary("Update current user profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
          () => NoContent(),
          failure => CustomResults.Problem(failure));
    }

    [HttpDelete("me")]
    [Authorize]
    [EndpointSummary("Delete current user account")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccount()
    {
        var result = await mediator.Send(new DeleteAccountCommand());
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpPatch("me/profile-picture")]
    [Authorize]
    [EndpointSummary("Update current user profile picture")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfilePicture([FromForm] UpdateProfilePictureCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpDelete("me/profile-picture")]
    [Authorize]
    [EndpointSummary("Delete current user profile picture")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProfilePicture()
    {
        var result = await mediator.Send(new DeleteProfilePictureCommand());
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost("me/change-password")]
    [Authorize]
    [EndpointSummary("Change current user password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => NoContent(),
            failure => CustomResults.Problem(failure));

    }

    [HttpGet("{username}")]
    [EndpointSummary("Get User Profile")]
    [ProducesResponseType(typeof(ApiResponse<UserProfileDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserProfileByUsername(string username)
    {
        var result = await mediator.Send(new GetProfileByUsernameQuery(username));

        return result.Match(
           profile => Ok(ApiResponse<UserProfileDto>.Success(profile)),
           failure => CustomResults.Problem(failure));
    }

    [HttpGet("search")]
    [EndpointSummary("Get All Users with search")]
    [ProducesResponseType(typeof(PagedResult<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsers([FromQuery] QueryParameters parameters)
    {
        var result = await mediator.Send(new GetAllUsersQuery(parameters));
        return Ok(result);
    }

    [HttpGet("me/shelves")]
    [Authorize]
    [EndpointSummary("Get shelves for the current user")]
    [ProducesResponseType(typeof(PagedResult<ShelfDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyShelves([FromQuery] QueryParameters parameters)
    {
        var userId = userContext.UserId;
        if (userId is null)
            return Unauthorized();

        var result = await mediator.Send(new GetUserShelvesQuery(userId, parameters));
        return Ok(result);
    }

    [HttpGet("{userId}/shelves")]
    [EndpointSummary("Get shelves for a specific user by ID")]
    [ProducesResponseType(typeof(PagedResult<ShelfDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserShelves(string userId, [FromQuery] QueryParameters parameters)
    {
        var result = await mediator.Send(new GetUserShelvesQuery(userId, parameters));
        return Ok(result);
    }


}