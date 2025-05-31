using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.ReadingProgresses.Queries.GetReadingProgresses;

internal class GetReadingProgressesQueryHandler : IRequestHandler<GetReadingProgressesQuery, PagedResult<ReadingProgressDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<GetReadingProgressesQueryHandler> _logger;

    public GetReadingProgressesQueryHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        IMapper mapper,
        ILogger<GetReadingProgressesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<ReadingProgressDto>> Handle(GetReadingProgressesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        var result = await _unitOfWork.ReadingProgresses.GetAllAsync(
            filter: rp => rp.UserId == userId,
            includes: new[] { "Book" },
            sortColumn: request.Parameters.SortColumn,
            sortOrder: request.Parameters.SortOrder,
            pageNumber: request.Parameters.PageNumber,
            pageSize: request.Parameters.PageSize);

        var dtoList = _mapper.Map<List<ReadingProgressDto>>(result.Items);

        _logger.LogInformation("Returned {Count} reading progresses for user {UserId}", dtoList.Count, userId);

        return PagedResult<ReadingProgressDto>.Create(dtoList, request.Parameters.PageNumber, request.Parameters.PageSize, result.Count);
    }
}