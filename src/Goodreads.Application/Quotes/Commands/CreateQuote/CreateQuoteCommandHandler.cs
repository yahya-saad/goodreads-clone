namespace Goodreads.Application.Quotes.Commands.CreateQuote;
public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateQuoteCommandHandler> _logger;

    public CreateQuoteCommandHandler(
        IUnitOfWork unitOfWork,
        IUserContext userContext,
        IMapper mapper,
        ILogger<CreateQuoteCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId == null)
            return Result<string>.Fail(AuthErrors.Unauthorized);

        _logger.LogInformation("Creating quote by user {UserId}", userId);

        var quote = _mapper.Map<Quote>(request);
        quote.CreatedByUserId = userId;

        await _unitOfWork.Quotes.AddAsync(quote);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Quote {QuoteId} created successfully by user {UserId}", quote.Id, userId);

        return Result<string>.Ok(quote.Id);
    }
}
