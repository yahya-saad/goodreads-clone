using System.Linq.Expressions;

namespace Goodreads.Application.Shelves.Queries.GetUserShelves;
internal class GetUserShelvesQueryHandler : IRequestHandler<GetUserShelvesQuery, PagedResult<ShelfDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserShelvesQueryHandler> _logger;

    public GetUserShelvesQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetUserShelvesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<ShelfDto>> Handle(GetUserShelvesQuery request, CancellationToken cancellationToken)
    {
        var p = request.Parameters;
        Expression<Func<Shelf, bool>> filter = s => s.UserId == request.UserId &&
            (string.IsNullOrEmpty(request.Shelf) || s.Name == request.Shelf);

        var (shelves, totalCount) = await _unitOfWork.Shelves.GetAllAsync(
            filter: filter,
            includes: new[] { "BookShelves.Book.Author", "BookShelves.Book.BookGenres.Genre" },
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize
        );

        _logger.LogInformation("Retrieved {Count} shelves for user {UserId} with shelf filter '{Shelf}'", totalCount, request.UserId, request.Shelf);

        var dtoList = _mapper.Map<List<ShelfDto>>(shelves);
        return PagedResult<ShelfDto>.Create(dtoList, p.PageNumber, p.PageSize, totalCount);
    }
}