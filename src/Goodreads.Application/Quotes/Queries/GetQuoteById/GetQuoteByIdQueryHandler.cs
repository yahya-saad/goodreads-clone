using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.DTOs;
using Goodreads.Domain.Errors;

namespace Goodreads.Application.Quotes.Queries.GetQuoteById;
internal class GetQuoteByIdQueryHandler : IRequestHandler<GetQuoteByIdQuery, Result<QuoteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetQuoteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<QuoteDto>> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
    {
        var quote = await _unitOfWork.Quotes.GetByIdAsync(request.Id);
        if (quote == null)
            return Result<QuoteDto>.Fail(QuoteErrors.NotFound(request.Id));

        var quoteDto = _mapper.Map<QuoteDto>(quote);
        return Result<QuoteDto>.Ok(quoteDto);
    }
}

