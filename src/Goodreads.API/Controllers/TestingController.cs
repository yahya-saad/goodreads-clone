using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class zTestingController(IEmailService emailService, IBlobStorageService _blobStorageService) : ControllerBase
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

    [HttpPost("image/upload")]
    public async Task<IActionResult> Upload(IFormFile file, BlobContainer container)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file provided.");

        using var stream = file.OpenReadStream();

        var url = await _blobStorageService.UploadAsync(file.FileName, stream, container);

        return Ok(new { Url = url });
    }

    [HttpGet("image/url")]
    public IActionResult GetSasUrl([FromQuery] string blobName)
    {
        if (string.IsNullOrEmpty(blobName))
            return BadRequest("blobUrl is required.");

        var sasUrl = _blobStorageService.GetUrl(blobName);

        if (sasUrl == null)
            return NotFound("Invalid blobUrl.");

        return Ok(new { SasUrl = sasUrl });
    }

    [HttpDelete("image/delete")]
    public async Task<IActionResult> Delete([FromQuery] string blobName)
    {
        if (string.IsNullOrEmpty(blobName))
            return BadRequest("blobUrl is required.");

        await _blobStorageService.DeleteAsync(blobName);

        return Ok(new { Message = "Deleted successfully." });
    }


}
