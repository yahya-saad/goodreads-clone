namespace Goodreads.Application.DTOs;

public class UserYearChallengeDto
{
    public int Year { get; set; }
    public int CompletedBooksCount { get; set; }
    public int TargetBooksCount { get; set; }
}

public class UserYearChallengeDetailsDto
{
    public int Year { get; set; }
    public int CompletedBooksCount { get; set; }
    public int TargetBooksCount { get; set; }
    public List<ChallengeBookDto> Books { get; set; } = new();
}


public class ChallengeBookDto
{
    public string BookId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string CoverImageUrl { get; set; } = default!;
    public string AuthorName { get; set; } = default!;
}