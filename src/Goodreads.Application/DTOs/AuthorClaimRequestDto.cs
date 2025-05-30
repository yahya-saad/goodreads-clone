namespace Goodreads.Application.DTOs;
public class AuthorClaimRequestDto
{
    public string Id { get; set; } = default!;

    public string AuthorId { get; set; } = default!;
    public string AuthorName { get; set; } = default!;

    public string UserId { get; set; } = default!;
    public string UserEmail { get; set; } = default!;

    public DateTime RequestedAt { get; set; }
    public ClaimRequestStatus Status { get; set; }

    public DateTime? ReviewedAt { get; set; }
    public string? ReviewedByAdminId { get; set; }
}

