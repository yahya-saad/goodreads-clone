using System.Linq.Expressions;

namespace Goodreads.Application.Authors.Queries.GetAllAuthors;
internal class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, PagedResult<AuthorDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllAuthorsQuery> _logger;

    public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllAuthorsQuery> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<PagedResult<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var p = request.Parameters;
        Expression<Func<Author, bool>> filter = a => string.IsNullOrEmpty(p.Query) || a.Name.Contains(p.Query);

        var (authors, count) = await _unitOfWork.Authors.GetAllAsync(
            filter: filter,
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize);

        var dtoList = _mapper.Map<List<AuthorDto>>(authors);

        _logger.LogInformation("Retrieved {Count} authors with query: {Query}", count, p.Query);

        return PagedResult<AuthorDto>.Create(dtoList, p.PageNumber, p.PageSize, count);
    }
}
