namespace Goodreads.Application.Reviews.Queries.GetReviewById;
public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Result<BookReviewDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReviewByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<BookReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _unitOfWork.BookReviews.GetByIdAsync(request.ReviewId, includes: new[] { "Book" });

        if (review is null)
            return Result<BookReviewDto>.Fail(Error.NotFound("BookReview.NotFound", "Review not found."));

        var dto = _mapper.Map<BookReviewDto>(review);
        return Result<BookReviewDto>.Ok(dto);
    }
}

