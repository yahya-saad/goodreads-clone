namespace Goodreads.Application.Reviews.Commands.CreateBookReview;
public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<string>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (string.IsNullOrEmpty(userId))
            return Result<string>.Fail(AuthErrors.Unauthorized);

        var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
        if (book is null)
            return Result<string>.Fail(BookErrors.NotFound(request.BookId));

        var existingReview = await _unitOfWork.BookReviews.GetSingleOrDefaultAsync(
            r => r.BookId == request.BookId && r.UserId == userId);

        if (existingReview != null)
            return Result<string>.Fail(BookReviewErrors.AlreadyReviewed(request.BookId));

        var review = new BookReview
        {
            BookId = request.BookId,
            UserId = userId,
            Rating = request.Rating,
            ReviewText = request.ReviewText
        };

        await _unitOfWork.BookReviews.AddAsync(review);

        var readShelf = await _unitOfWork.Shelves.GetSingleOrDefaultAsync(
            s => s.UserId == userId && s.Name == DefaultShelves.Read && s.IsDefault,
            includes: new[] { "BookShelves" });

        if (readShelf != null)
        {
            var alreadyExists = readShelf.BookShelves.Any(bs => bs.BookId == request.BookId);
            if (!alreadyExists)
            {
                readShelf.BookShelves.Add(new BookShelf
                {
                    BookId = request.BookId,
                    ShelfId = readShelf.Id,
                });
            }
        }

        // Update Year Challenge
        var currentYear = DateTime.UtcNow.Year;

        var userChallenge = (await _unitOfWork.UserYearChallenges.GetSingleOrDefaultAsync(
            c => c.UserId == userId && c.Year == currentYear));

        if (userChallenge != null)
        {
            var readBookCount = await _unitOfWork.BookShelves.CountAsync(bs =>
                bs.Shelf.UserId == userId &&
                bs.Shelf.IsDefault &&
                bs.Shelf.Name == DefaultShelves.Read &&
                bs.AddedAt.Year == currentYear);

            if (userChallenge.CompletedBooksCount != readBookCount)
            {
                userChallenge.CompletedBooksCount = readBookCount;
                _unitOfWork.UserYearChallenges.Update(userChallenge);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(review.Id);
    }

}
