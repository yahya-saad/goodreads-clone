using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Constants;
using Goodreads.Domain.Errors;
using SharedKernel;

namespace Goodreads.Application.Books.Commands.UpdateBookStatus;

public class UpdateBookStatusCommandHandler : IRequestHandler<UpdateBookStatusCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateBookStatusCommandHandler> _logger;

    public UpdateBookStatusCommandHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        ILogger<UpdateBookStatusCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }
    public async Task<Result> Handle(UpdateBookStatusCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var (statusShelves, count) = await _unitOfWork.Shelves.GetAllAsync(
            filter: s => s.UserId == userId && s.IsDefault,
            includes: new[] { "BookShelves" });

        foreach (var shelf in statusShelves)
        {
            var existing = shelf.BookShelves.FirstOrDefault(bs => bs.BookId == request.BookId);
            if (existing != null)
                shelf.BookShelves.Remove(existing);
        }

        if (string.IsNullOrWhiteSpace(request.TargetShelfName))
        {
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Book {BookId} removed from all default shelves", request.BookId);
            return Result.Ok();
        }

        var targetShelf = statusShelves.FirstOrDefault(s => s.Name == request.TargetShelfName);
        if (targetShelf == null)
        {
            _logger.LogWarning("Default shelf not found: {ShelfName}", request.TargetShelfName);
            return Result.Fail(Error.NotFound("Shelves.DefaultShelfNotFound", $"Default shelf '{request.TargetShelfName}' not found."));
        }

        targetShelf.BookShelves.Add(new BookShelf
        {
            BookId = request.BookId,
            ShelfId = targetShelf.Id,
            AddedAt = DateTime.UtcNow
        });

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Book {BookId} updated to status {ShelfName}", request.BookId, request.TargetShelfName);


        // تحديث التحدي السنوي 
        var currentYear = DateTime.UtcNow.Year;

        var readShelf = await _unitOfWork.Shelves.GetSingleOrDefaultAsync(
             filter: s => s.UserId == userId && s.Name == DefaultShelves.Read);

        if (readShelf == null)
            return Result.Fail(ShelfErrors.NotFound("Read Shelf"));

        var completedBooksCount = await _unitOfWork.BookShelves.CountAsync(
            bs => bs.ShelfId == readShelf.Id && bs.AddedAt.Year == currentYear);

        var userChallenge = await _unitOfWork.UserYearChallenges.GetSingleOrDefaultAsync(
            filter: c => c.UserId == userId && c.Year == currentYear);

        if (userChallenge != null && userChallenge.CompletedBooksCount != completedBooksCount)
        {
            userChallenge.CompletedBooksCount = completedBooksCount;
            _unitOfWork.UserYearChallenges.Update(userChallenge);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Updated yearly challenge completed count for user {UserId} to {Count}", userId, completedBooksCount);
        }

        return Result.Ok();
    }

}