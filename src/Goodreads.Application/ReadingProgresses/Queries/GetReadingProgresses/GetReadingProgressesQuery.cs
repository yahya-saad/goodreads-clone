namespace Goodreads.Application.ReadingProgresses.Queries.GetReadingProgresses;
public record GetReadingProgressesQuery(QueryParameters Parameters) : IRequest<PagedResult<ReadingProgressDto>>;
