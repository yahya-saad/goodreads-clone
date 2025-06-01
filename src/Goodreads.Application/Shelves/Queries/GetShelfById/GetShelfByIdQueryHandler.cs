namespace Goodreads.Application.Shelves.Queries.GetShelfById;

internal class GetShelfByIdQueryHandler : IRequestHandler<GetShelfByIdQuery, Result<ShelfDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetShelfByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetShelfByIdQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetShelfByIdQueryHandler> logger,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<ShelfDto>> Handle(GetShelfByIdQuery request, CancellationToken cancellationToken)
    {
        var shelf = await _unitOfWork.Shelves.GetByIdAsync(request.Id, "BookShelves.Book.Author", "BookShelves.Book.BookGenres.Genre");
        if (shelf == null)
        {
            _logger.LogWarning("Shelf with ID {Id} not found", request.Id);
            return Result<ShelfDto>.Fail(ShelfErrors.NotFound(request.Id));
        }

        var dto = _mapper.Map<ShelfDto>(shelf);

        _logger.LogInformation("Retrieved shelf with ID {Id}", request.Id);
        return Result<ShelfDto>.Ok(dto);
    }
}
