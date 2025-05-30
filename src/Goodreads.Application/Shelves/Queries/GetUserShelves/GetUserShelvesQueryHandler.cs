using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

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

        var (shelves, totalCount) = await _unitOfWork.Shelves.GetAllAsync(
            filter: s => s.UserId == request.UserId,
            includes: new[] { "BookShelves.Book.Author", "BookShelves.Book.BookGenres.Genre" },
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize
        );

        _logger.LogInformation("Retrieved {Count} shelves for user {UserId}", totalCount, request.UserId);

        var dtoList = _mapper.Map<List<ShelfDto>>(shelves);
        return PagedResult<ShelfDto>.Create(dtoList, p.PageNumber, p.PageSize, totalCount);
    }
}