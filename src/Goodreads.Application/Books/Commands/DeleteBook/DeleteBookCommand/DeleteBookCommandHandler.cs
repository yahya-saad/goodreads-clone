namespace Goodreads.Application.Books.Commands.DeleteBook.DeleteBookCommand;
internal class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteBookCommandHandler> _logger;
    public DeleteBookCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteBookCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteBookCommand for book with ID: {BookId}", request.Id);
        var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
        if (book == null)
        {
            _logger.LogWarning("Book with ID: {BookId} not found", request.Id);
            return Result.Fail(BookErrors.NotFound(request.Id));
        }
        _unitOfWork.Books.Delete(book);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Successfully deleted book with ID: {BookId}", request.Id);
        return Result.Ok();


    }
}
