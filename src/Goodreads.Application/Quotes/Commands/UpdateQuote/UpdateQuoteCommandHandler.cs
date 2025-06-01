namespace Goodreads.Application.Quotes.Commands.UpdateQuote;

public class UpdateQuoteCommandHandler : IRequestHandler<UpdateQuoteCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly ILogger<UpdateQuoteCommandHandler> _logger;

    public UpdateQuoteCommandHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        ILogger<UpdateQuoteCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateQuoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result.Fail(AuthErrors.Unauthorized);

        var quote = await _unitOfWork.Quotes.GetByIdAsync(request.QuoteId);
        if (quote == null)
            return Result.Fail(QuoteErrors.NotFound(request.QuoteId));

        quote.Text = request.Text;

        if (request.Tags != null)
            quote.Tags = request.Tags;

        _unitOfWork.Quotes.Update(quote);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Quote {QuoteId} updated by user {UserId}.", quote.Id, userId);
        return Result.Ok();
    }
}