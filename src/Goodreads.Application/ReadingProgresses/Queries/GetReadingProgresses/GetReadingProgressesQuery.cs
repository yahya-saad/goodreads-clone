using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.ReadingProgresses.Queries.GetReadingProgresses;
public record GetReadingProgressesQuery(QueryParameters Parameters) : IRequest<PagedResult<ReadingProgressDto>>;
