namespace Goodreads.Application.Shelves.Commands.AddBookToShelf;
internal class AddBookToShelfCommandHandler : IRequestHandler<AddBookToShelfCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddBookToShelfCommandHandler> _logger;

    public AddBookToShelfCommandHandler(IUnitOfWork unitOfWork, ILogger<AddBookToShelfCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(AddBookToShelfCommand request, CancellationToken cancellationToken)
    {

        var shelf = await _unitOfWork.Shelves.GetByIdAsync(request.ShelfId, "BookShelves");
        if (shelf == null)
        {
            _logger.LogWarning("Shelf {ShelfId} not found", request.ShelfId);
            return Result.Fail(ShelfErrors.NotFound(request.ShelfId));
        }


        var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
        if (book == null)
        {
            _logger.LogWarning("Book {BookId} not found", request.BookId);
            return Result.Fail(BookErrors.NotFound(request.BookId));
        }

        if (shelf.IsDefault) return Result.Fail(ShelfErrors.DefaultShelfAddDenied(shelf.Name));

        var toAdd = new BookShelf
        {
            ShelfId = shelf.Id,
            BookId = book.Id,
            AddedAt = DateTime.UtcNow
        };

        shelf.BookShelves.Add(toAdd);

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Book {BookId} added to Shelf {ShelfId}", request.BookId, request.ShelfId);

        return Result.Ok();
    }
}