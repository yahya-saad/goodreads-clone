namespace Goodreads.Application.ReadingProgresses.Commands.UpdateReadingProgress;
public record UpdateReadingProgressCommand(string BookId, int CurrentPage) : IRequest<Result>;
