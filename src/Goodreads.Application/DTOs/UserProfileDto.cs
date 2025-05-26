namespace Goodreads.Application.DTOs;
public class UserProfileDto
{
    public string Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Country { get; set; }
    public SocialDto? Social { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class SocialDto
{
    public string? Facebook { get; set; }
    public string? Twitter { get; set; }
    public string? Linkedin { get; set; }
}