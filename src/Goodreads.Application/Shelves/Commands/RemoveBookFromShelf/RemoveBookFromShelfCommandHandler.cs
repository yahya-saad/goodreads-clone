namespace Goodreads.Application.Shelves.Commands.RemoveBookFromShelf;
internal class RemoveBookFromShelfCommandHandler : IRequestHandler<RemoveBookFromShelfCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveBookFromShelfCommandHandler> _logger;

    public RemoveBookFromShelfCommandHandler(IUnitOfWork unitOfWork, ILogger<RemoveBookFromShelfCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(RemoveBookFromShelfCommand request, CancellationToken cancellationToken)
    {
        var shelf = await _unitOfWork.Shelves.GetByIdAsync(request.ShelfId, "BookShelves");
        if (shelf == null)
        {
            _logger.LogWarning("Shelf {ShelfId} not found", request.ShelfId);
            return Result.Fail(ShelfErrors.NotFound(request.ShelfId));
        }



        var bookShelf = shelf.BookShelves.FirstOrDefault(bs => bs.BookId == request.BookId);
        if (bookShelf == null)
        {
            return Result.Fail(Error.NotFound(
                "Shelf.Notfound",
                $"Book:{request.BookId} not found in shelf:{shelf.Name}"));
        }

        shelf.BookShelves.Remove(bookShelf);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Book {BookId} removed from Shelf {ShelfId}", request.BookId, request.ShelfId);

        return Result.Ok();
    }
}