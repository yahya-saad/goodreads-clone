using Goodreads.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestingController(IEmailService emailService) : ControllerBase
{
    [HttpGet("sending-emails")]
    [EndpointSummary("Send a test email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SendTestEmail(string to, string subject, string message)
    {
        await emailService.SendEmailAsync(to, subject, message);
        return Ok();
    }
}
