using Goodreads.Application.Common.Interfaces;
using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;
using System.Linq.Expressions;

namespace Goodreads.Application.Genres.Queries.GetAllGenres;

internal class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, PagedResult<GenreDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllGenresQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllGenresQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllGenresQueryHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PagedResult<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var p = request.Parameters;
        Expression<Func<Genre, bool>> filter = g => string.IsNullOrEmpty(p.Query) || g.Name.Contains(p.Query);

        var result = await _unitOfWork.Genres.GetAllAsync(
            filter: filter,
            includes: new[] { "BookGenres" },
            sortColumn: p.SortColumn,
            sortOrder: p.SortOrder,
            pageNumber: p.PageNumber,
            pageSize: p.PageSize);

        var genres = result.Items.ToList();
        var count = result.Count;

        _logger.LogInformation("Retrieved {Count} genres with query: {Query}", count, p.Query);

        var dtoList = _mapper.Map<List<GenreDto>>(genres);

        return PagedResult<GenreDto>.Create(dtoList, p.PageNumber, p.PageSize, count);
    }
}
