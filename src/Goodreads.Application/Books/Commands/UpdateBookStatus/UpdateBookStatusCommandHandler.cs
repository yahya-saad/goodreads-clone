using Goodreads.Application.Common.Interfaces;
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


        // تحديث التحدي السنوي إذا موجود
        var currentYear = DateTime.UtcNow.Year;

        var (readShelves, _) = await (_unitOfWork.Shelves.GetAllAsync(
             filter: s => s.UserId == userId && s.Name == "Read"));
        var readShelf = readShelves.FirstOrDefault();

        var completedBooksCount = await _unitOfWork.BookShelves.CountAsync(
            bs => bs.ShelfId == readShelf!.Id && bs.AddedAt.Year == currentYear);


        var (challenge, _) = await _unitOfWork.UserYearChallenges.GetAllAsync(
            filter: c => c.UserId == userId && c.Year == currentYear);

        var userChallenge = challenge.FirstOrDefault();
        if (userChallenge != null)
        {
            if (userChallenge.CompletedBooksCount != completedBooksCount)
            {
                userChallenge.CompletedBooksCount = completedBooksCount;
                _unitOfWork.UserYearChallenges.Update(userChallenge);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Updated yearly challenge completed count for user {UserId} to {Count}", userId, completedBooksCount);
            }
        }

        return Result.Ok();
    }

}