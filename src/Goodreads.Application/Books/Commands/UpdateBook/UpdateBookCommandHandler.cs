namespace Goodreads.Application.Books.Commands.UpdateBook;
internal class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateBookCommandHandler> _logger;
    private readonly IMapper _mapper;
    public UpdateBookCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateBookCommandHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetByIdAsync(request.Id, "Author", "BookGenres");

        if (book == null)
        {
            _logger.LogWarning("Book with ID: {BookId} not found", request.Id);
            return Result.Fail(BookErrors.NotFound(request.Id));
        }

        _mapper.Map(request, book);

        if (request.PageCount is not null)
            book.PageCount = request.PageCount.Value;

        if (request.AuthorId is not null)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);
            if (author == null)
            {
                _logger.LogWarning("Author with ID: {AuthorId} not found", request.AuthorId);
                return Result<string>.Fail(AuthorErrors.NotFound(request.AuthorId));
            }
            book.Author = author;
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();

    }
}
