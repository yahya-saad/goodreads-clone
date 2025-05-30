using Goodreads.Domain.Entities;
using System.Text.Json.Serialization;

public class AuthorClaimRequest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string AuthorId { get; set; } = default!;
    public Author Author { get; set; } = default!;

    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public ClaimRequestStatus Status { get; set; } = ClaimRequestStatus.Pending;
    public DateTime? ReviewedAt { get; set; }
    public string? ReviewedByAdminId { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ClaimRequestStatus
{
    Pending,
    Approved,
    Rejected
}
