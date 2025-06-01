namespace Goodreads.Application.Authors.Queries.GetAuthorById;
public record GetAuthorByIdQuery(string Id) : IRequest<Result<AuthorDto>>;
