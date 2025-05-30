using Goodreads.Application.Common.Interfaces;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Quotes.Commands.DeleteQuote;
internal class DeleteQuoteCommandHandler : IRequestHandler<DeleteQuoteCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteQuoteCommandHandler> _logger;
    public DeleteQuoteCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteQuoteCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task<Result> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
    {
        var quote = await _unitOfWork.Quotes.GetByIdAsync(request.QuoteId);
        if (quote == null)
        {
            _logger.LogWarning("Quote with ID {QuoteId} not found for deletion.", request.QuoteId);
            return Result.Fail(QuoteErrors.NotFound(request.QuoteId));
        }
        _unitOfWork.Quotes.Delete(quote);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok();
    }
}
