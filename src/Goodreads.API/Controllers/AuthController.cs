using Goodreads.API.Common;
using Goodreads.Application.Auth.Commands.ConfirmEmail;
using Goodreads.Application.Auth.Commands.ForgotPassword;
using Goodreads.Application.Auth.Commands.LoginUser;
using Goodreads.Application.Auth.Commands.Logout;
using Goodreads.Application.Auth.Commands.RefreshToken;
using Goodreads.Application.Auth.Commands.RegisterUser;
using Goodreads.Application.Auth.Commands.ResetEmailConfirmation;
using Goodreads.Application.Auth.Commands.ResetPassword;
using Goodreads.Application.Common;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        return result.Match(
        success => Ok(ApiResponse<string>.Success(success, "Registration successful! Please check your email to confirm your account.")),
        failure => CustomResults.Problem(failure));
    }

    [HttpPost("login")]
    [EndpointSummary("Login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match(
            success => Ok(ApiResponse<AuthResultDto>.Success(success, "Login successful")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost("refresh")]
    [EndpointSummary("Refresh user token")]
    [ProducesResponseType(typeof(ApiResponse<AuthResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            success => Ok(ApiResponse<AuthResultDto>.Success(success, "Token refreshed successfully")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost("logout")]
    [Authorize]
    [EndpointSummary("Logout")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        var result = await mediator.Send(command);
        return result.Match(
            () => Ok(ApiResponse.Success("Logout successful")),
            failure => CustomResults.Problem(failure));
    }


    [HttpPost("confirm-email")]
    [EndpointSummary("Confirm user email address")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        var result = await mediator.Send(new ConfirmEmailCommand(userId, token));

        return result.Match(
            success => Ok(ApiResponse.Success("Email confirmed successfully.")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost("reset-confirmation-email")]
    [EndpointSummary("Generate a new email confirmation link")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetEmailConfirmation([FromBody] ResetEmailConfirmationCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match(
            success => Ok(ApiResponse<string>.Success(success, "New confirmation email link generated.")),
            failure => CustomResults.Problem(failure));
    }

    [HttpPost("forgot-password")]
    [EndpointSummary("Request password reset link")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        var result = await mediator.Send(command);

        return result.Match(
            resetLink => Ok(ApiResponse<string>.Success(resetLink, "password reset link sent.")),
            onFailure => CustomResults.Problem(onFailure));
    }


    [HttpPost("reset-password")]
    [EndpointSummary("Reset user password")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromQuery] string userId, [FromQuery] string token, [FromBody] string NewPassword)
    {
        var result = await mediator.Send(new ResetPasswordCommand(userId, token, NewPassword));
        return result.Match(
            () => Ok(ApiResponse.Success("password reset successfully.")),
            onFailure => CustomResults.Problem(onFailure));
    }

}
