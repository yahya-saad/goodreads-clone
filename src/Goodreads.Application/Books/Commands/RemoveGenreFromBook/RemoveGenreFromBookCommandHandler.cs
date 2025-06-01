namespace Goodreads.Application.Books.Commands.RemoveGenreFromBook;
internal class RemoveGenreFromBookCommandHandler : IRequestHandler<RemoveGenreFromBookCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveGenreFromBookCommandHandler> _logger;

    public RemoveGenreFromBookCommandHandler(IUnitOfWork unitOfWork, ILogger<RemoveGenreFromBookCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(RemoveGenreFromBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(request.BookId, "BookGenres");
        if (book == null)
        {
            _logger.LogWarning("Book with ID: {BookId} not found", request.BookId);
            return Result.Fail(BookErrors.NotFound(request.BookId));
        }

        var bookGenre = book.BookGenres.FirstOrDefault(bg => bg.GenreId == request.GenreId);
        if (bookGenre == null)
        {
            _logger.LogWarning("Genre with ID: {GenreId} is not associated with Book ID: {BookId}", request.GenreId, request.BookId);
            return Result.Fail(GenreErrors.NotFound(request.GenreId));
        }

        book.BookGenres.Remove(bookGenre);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}