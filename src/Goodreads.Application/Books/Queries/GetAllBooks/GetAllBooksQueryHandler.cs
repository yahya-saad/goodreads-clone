using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using System.Linq.Expressions;

namespace Goodreads.Application.Books.Queries.GetAllBooks;
internal class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, PagedResult<BookDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllBooksQueryHandler> _logger;
    private readonly IMapper _mapper;
    public GetAllBooksQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllBooksQueryHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<PagedResult<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllBooksQuery with parameters: {@Parameters}", request.Parameters);
        var p = request.Parameters;

        Expression<Func<Book, bool>> filter = book =>
            string.IsNullOrEmpty(p.Query)
            || book.Title.Contains(p.Query)
            || book.Author.Name.Contains(p.Query)
            || book.BookGenres.Any(bg => bg.Genre.Name.Contains(p.Query));

        var (books, totalCount) = await _unitOfWork.Books.GetAllAsync(
            filter: filter,
            includes: new[] { "Author", "BookGenres.Genre" },
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize
        );

        var bookDtos = _mapper.Map<List<BookDto>>(books);

        return PagedResult<BookDto>.Create(bookDtos, p.PageNumber, p.PageSize, totalCount);
    }
}
