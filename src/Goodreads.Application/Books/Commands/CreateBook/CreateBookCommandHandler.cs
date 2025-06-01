namespace Goodreads.Application.Books.Commands.CreateBook;
public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBookCommandHandler> _logger;
    private readonly IBlobStorageService _blobStorageService;
    public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateBookCommandHandler> logger, IBlobStorageService blobStorageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _blobStorageService = blobStorageService;
    }
    public async Task<Result<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId);
        if (author == null)
        {
            _logger.LogWarning("Author with ID: {AuthorId} not found", request.AuthorId);
            return Result<string>.Fail(AuthorErrors.NotFound(request.AuthorId));
        }

        var book = _mapper.Map<Book>(request);
        book.Author = author;

        if (request.CoverImage != null)
        {
            using var stream = request.CoverImage.OpenReadStream();
            var (url, blobName) = await _blobStorageService.UploadAsync(request.CoverImage.FileName, stream, BlobContainer.Books);
            book.CoverImageUrl = url;
            book.CoverImageBlobName = blobName;
        }

        await _unitOfWork.Books.AddAsync(book);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Book created successfully with ID: {BookId}", book.Id);
        return Result<string>.Ok(book.Id);
    }
}
