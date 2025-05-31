namespace Goodreads.Application.DTOs;
public class ReadingProgressDto
{
    public string BookId { get; set; }
    public string Title { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public double ProgressPercent { get; set; }
    public DateTime UpdatedAt { get; set; }
}

