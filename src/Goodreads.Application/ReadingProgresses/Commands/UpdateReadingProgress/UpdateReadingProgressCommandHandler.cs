namespace Goodreads.Application.ReadingProgresses.Commands.UpdateReadingProgress;

public class UpdateReadingProgressCommandHandler : IRequestHandler<UpdateReadingProgressCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateReadingProgressCommandHandler> _logger;

    public UpdateReadingProgressCommandHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        ILogger<UpdateReadingProgressCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateReadingProgressCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        var progress = await _unitOfWork.ReadingProgresses.GetSingleOrDefaultAsync(
            filter: rp => rp.UserId == userId && rp.BookId == request.BookId);

        var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
        if (book == null)
            return Result.Fail(BookErrors.NotFound(request.BookId));

        if (progress == null)
        {
            progress = new ReadingProgress
            {
                UserId = userId,
                BookId = request.BookId,
                CurrentPage = request.CurrentPage,
            };
            await _unitOfWork.ReadingProgresses.AddAsync(progress);
        }
        else
        {
            progress.CurrentPage = request.CurrentPage;
        }


        // If finished the book, add it to "Read" shelf automatically
        if (request.CurrentPage == book.PageCount)
        {
            var (shelves, _) = await _unitOfWork.Shelves.GetAllAsync(
                filter: s => s.UserId == userId && s.IsDefault,
                includes: new[] { "BookShelves" });

            var readShelf = shelves.FirstOrDefault(s => s.Name == DefaultShelves.Read);
            var currentlyReadingShelf = shelves.FirstOrDefault(s => s.Name == DefaultShelves.CurrentlyReading);


            // Remove from Currently Reading 
            var existing = currentlyReadingShelf.BookShelves.FirstOrDefault(bs => bs.BookId == book.Id);
            if (existing != null)
                currentlyReadingShelf.BookShelves.Remove(existing);

            // Add to Read shelf 
            readShelf.BookShelves.Add(new BookShelf
            {
                BookId = book.Id,
                ShelfId = readShelf.Id,
                AddedAt = DateTime.UtcNow
            });

            // Update Year Challenge
            var currentYear = DateTime.UtcNow.Year;
            var userChallenge = await _unitOfWork.UserYearChallenges.GetSingleOrDefaultAsync(
             filter: c => c.UserId == userId && c.Year == currentYear);

            if (userChallenge != null)
                userChallenge.CompletedBooksCount += 1;

        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Updated reading progress for book {BookId} to page {Page}", request.BookId, request.CurrentPage);

        return Result.Ok();
    }
}