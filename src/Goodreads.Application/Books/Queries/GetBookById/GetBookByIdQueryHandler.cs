using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.DTOs;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Books.Queries.GetBookById;
internal class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Result<BookDetailDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBookByIdQueryHandler> _logger;
    public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetBookByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Result<BookDetailDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetBookByIdQuery for book with ID: {BookId}", request.Id);
        var book = await _unitOfWork.Books.GetByIdAsync(request.Id, "Author", "BookGenres.Genre");
        if (book == null)
        {
            _logger.LogWarning("Book with ID: {BookId} not found", request.Id);
            return Result<BookDetailDto>.Fail(BookErrors.NotFound(request.Id));
        }

        var bookDetailDto = _mapper.Map<BookDetailDto>(book);
        _logger.LogInformation("Successfully retrieved book details for ID: {BookId}", request.Id);
        return Result<BookDetailDto>.Ok(bookDetailDto);
    }
}
