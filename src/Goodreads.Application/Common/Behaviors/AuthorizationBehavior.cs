using Goodreads.Application.Common.Interfaces.Authorization;

namespace Goodreads.Application.Common.Behaviors;
public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IAuthorAuthorizationService _authorAuthService;
    private readonly IShelfAuthorizationService _shelfAuthService;
    private readonly IQuoteAuthorizationService _quoteAuthService;
    private readonly IReviewAuthorizationService _reviewAuthService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorizationBehavior(IAuthorAuthorizationService authorizationService, IShelfAuthorizationService shelfAuthService, IUnitOfWork unitOfWork, IQuoteAuthorizationService quoteAuthService, IReviewAuthorizationService reviewAuthService)
    {
        _authorAuthService = authorizationService;
        _shelfAuthService = shelfAuthService;
        _unitOfWork = unitOfWork;
        _quoteAuthService = quoteAuthService;
        _reviewAuthService = reviewAuthService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is IRequireAuthorAuthorization authRequest)
        {
            var authorized = await _authorAuthService.IsAuthorOwnerOrAdminAsync(authRequest.AuthorId);
            if (!authorized)
                throw new UnauthorizedAccessException();
        }

        if (request is IRequireBookAuthorization bookAuthRequest)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookAuthRequest.BookId);
            if (book == null || book.AuthorId == null)
                throw new UnauthorizedAccessException();

            var authorized = await _authorAuthService.IsAuthorOwnerOrAdminAsync(book.AuthorId);
            if (!authorized)
                throw new UnauthorizedAccessException();
        }

        if (request is IRequireShelfAuthorization shelfRequest)
        {
            var authorized = await _shelfAuthService.IsOwnerAsync(shelfRequest.ShelfId);
            if (!authorized)
                throw new UnauthorizedAccessException();
        }

        if (request is IRequireQuoteAuthorization quoteRequest)
        {
            var authorized = await _quoteAuthService.IsOwnerOrAdminAsync(quoteRequest.QuoteId);
            if (!authorized)
                throw new UnauthorizedAccessException();
        }

        if (request is IRequireReviewAuthorization reviewRequet)
        {
            var authorized = await _reviewAuthService.Authorize(reviewRequet.ReviewId);
            if (!authorized)
                throw new UnauthorizedAccessException();
        }

        return await next();
    }
}


