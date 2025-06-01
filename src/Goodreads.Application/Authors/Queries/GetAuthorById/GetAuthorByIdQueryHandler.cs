namespace Goodreads.Application.Authors.Queries.GetAuthorById;
internal class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Result<AuthorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAuthorByIdQueryHandler> _logger;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAuthorByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
        if (author == null)
        {
            _logger.LogWarning("Author with ID {Id} not found", request.Id);
            return Result<AuthorDto>.Fail(AuthorErrors.NotFound(request.Id));
        }

        var dto = _mapper.Map<AuthorDto>(author);

        _logger.LogInformation("Retrieved author with ID {Id}", request.Id);
        return Result<AuthorDto>.Ok(dto);
    }
}
