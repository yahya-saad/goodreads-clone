using Goodreads.Application.Auth.Commands;
using Goodreads.Application.Auth.Commands.LoginUser;
using Goodreads.Application.Common.Responses;
using Goodreads.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{

    [HttpPost("register")]
    [EndpointSummary("Register a new user")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await mediator.Send(command);

        if (!result.Success)
            return BadRequest(ApiResponse.Failure(result.ErrorMessage!));

        return Ok(ApiResponse<string>.Success(result.Data, "User registered successfully"));
    }

    [HttpPost("login")]
    [EndpointSummary("Login an existing user")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await mediator.Send(command);

        if (!result.Success)
            return Unauthorized(ApiResponse.Failure(result.ErrorMessage!));

        return Ok(ApiResponse<User>.Success(result.Data, "Login successful"));
    }

}
