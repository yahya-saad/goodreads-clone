namespace Goodreads.Application.Authors.Queries.GetAllAuthors;
public record GetAllAuthorsQuery(QueryParameters Parameters) : IRequest<PagedResult<AuthorDto>>;
