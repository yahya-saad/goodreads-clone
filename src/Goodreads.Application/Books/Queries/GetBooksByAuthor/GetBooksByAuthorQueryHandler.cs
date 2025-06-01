using System.Linq.Expressions;

namespace Goodreads.Application.Books.Queries.GetBooksByAuthor;
internal class GetBooksByAuthorQueryHandler : IRequestHandler<GetBooksByAuthorQuery, PagedResult<BookDto>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBooksByAuthorQueryHandler> _logger;

    public GetBooksByAuthorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetBooksByAuthorQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<BookDto>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        var p = request.Parameters;
        _logger.LogInformation("Getting All Book for Author {AuthorId}", request.AuthorId);

        Expression<Func<Book, bool>> filter = b => b.AuthorId == request.AuthorId;

        string[] includes = new[] { "Author", "BookGenres.Genre" };

        var (books, totalCount) = await _unitOfWork.Books.GetAllAsync(
            filter: filter,
            includes: includes,
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize
        );

        var bookDtos = _mapper.Map<List<BookDto>>(books);

        var pagedResult = PagedResult<BookDto>.Create(bookDtos, p.PageNumber, p.PageSize, totalCount);

        return pagedResult;
    }
}
