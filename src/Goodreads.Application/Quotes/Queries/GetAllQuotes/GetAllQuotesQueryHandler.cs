using System.Linq.Expressions;

namespace Goodreads.Application.Quotes.Queries.GetAllQuotes;
public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, PagedResult<QuoteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllQuotesQueryHandler> _logger;

    public GetAllQuotesQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetAllQuotesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<QuoteDto>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
    {
        var p = request.Parameters;
        Expression<Func<Quote, bool>> filter = q =>
            (string.IsNullOrEmpty(p.Query) || q.Text.Contains(p.Query)) &&
            (string.IsNullOrEmpty(request.Tag) || q.Tags.Any(t => t == request.Tag)) &&
            (string.IsNullOrEmpty(request.UserId) || q.CreatedByUserId == request.UserId) &&
            (string.IsNullOrEmpty(request.AuthorId) || q.AuthorId == request.AuthorId) &&
            (string.IsNullOrEmpty(request.BookId) || q.BookId == request.BookId);

        var (quotes, count) = await _unitOfWork.Quotes.GetAllAsync(
            filter: filter,
            includes: new[] { "Likes" },
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize
        );


        var dtoList = _mapper.Map<List<QuoteDto>>(quotes);
        var pagedResult = PagedResult<QuoteDto>.Create(dtoList, p.PageNumber, p.PageSize, count);

        return pagedResult;
    }
}
