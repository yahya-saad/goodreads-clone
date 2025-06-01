using System.Linq.Expressions;

namespace Goodreads.Application.AuthorClaims.Queries.GetAllAuthorClaimRequests;
public class GetAllAuthorClaimRequestsQueryHandler : IRequestHandler<GetAllAuthorClaimRequestsQuery, PagedResult<AuthorClaimRequestDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllAuthorClaimRequestsQueryHandler> _logger;

    public GetAllAuthorClaimRequestsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllAuthorClaimRequestsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<AuthorClaimRequestDto>> Handle(GetAllAuthorClaimRequestsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<AuthorClaimRequest, bool>>? filter = request.Status.HasValue ? r => r.Status == request.Status : null;
        var includes = new[] { "Author", "User" };


        var (items, totalCount) = await _unitOfWork.AuthorClaimRequests.GetAllAsync(
            filter: filter,
            includes: includes,
            sortColumn: request.SortColumn,
            sortOrder: request.SortOrder,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize
        );

        _logger.LogInformation("Fetched author claim requests (Total: {TotalCount})", totalCount);

        var dtos = _mapper.Map<List<AuthorClaimRequestDto>>(items);

        return PagedResult<AuthorClaimRequestDto>.Create(dtos, request.PageNumber, request.PageSize, totalCount);
    }
}

