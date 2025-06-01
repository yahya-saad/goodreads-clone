namespace Goodreads.Application.Authors.Commands.CreateAuthor;
internal class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAuthorCommandHandler> _logger;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IMapper _mapper;
    public CreateAuthorCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateAuthorCommandHandler> logger, IBlobStorageService blobStorageService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _blobStorageService = blobStorageService;
        _mapper = mapper;
    }
    public async Task<Result<string>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = _mapper.Map<Author>(request);

        using var stream = request.ProfilePicture.OpenReadStream();
        var (url, blobName) = await _blobStorageService.UploadAsync(request.ProfilePicture.FileName, stream, BlobContainer.Authors);

        author.ProfilePictureUrl = url;
        author.ProfilePictureBlobName = blobName;

        await _unitOfWork.Authors.AddAsync(author);
        await _unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(author.Id);
    }
}
