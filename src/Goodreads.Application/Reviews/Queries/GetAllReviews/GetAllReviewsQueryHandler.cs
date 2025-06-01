using System.Linq.Expressions;

namespace Goodreads.Application.Reviews.Queries.GetAllReviews;
public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, PagedResult<BookReviewDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedResult<BookReviewDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<BookReview, bool>> filter = r =>
            (string.IsNullOrEmpty(request.UserId) || r.UserId == request.UserId) &&
            (string.IsNullOrEmpty(request.Bookid) || r.BookId == request.Bookid);

        var (items, count) = await _unitOfWork.BookReviews.GetAllAsync(
            filter: filter,
            includes: new[] { "Book" },
            sortColumn: request.Parameters.SortColumn,
            sortOrder: request.Parameters.SortOrder,
            pageNumber: request.Parameters.PageNumber,
            pageSize: request.Parameters.PageSize
        );

        var dtoList = _mapper.Map<List<BookReviewDto>>(items);

        return PagedResult<BookReviewDto>.Create(dtoList, request.Parameters.PageNumber, request.Parameters.PageSize, count);
    }
}
