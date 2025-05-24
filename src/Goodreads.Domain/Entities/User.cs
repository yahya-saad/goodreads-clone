using Microsoft.AspNetCore.Identity;

namespace Goodreads.Domain.Entities;
public class User : IdentityUser
{
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public DateOnly? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; } = default!;
    public string? Bio { get; set; } = default!;
    public string? WebsiteUrl { get; set; }
    public string? Country { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
