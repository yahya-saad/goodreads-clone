namespace Goodreads.Domain.Entities;
public class UserYearChallenge
{
    public string UserId { get; set; }
    public int Year { get; set; }

    public int TargetBooksCount { get; set; } = 0;
    public int CompletedBooksCount { get; set; } = 0;

    public User User { get; set; }
}
