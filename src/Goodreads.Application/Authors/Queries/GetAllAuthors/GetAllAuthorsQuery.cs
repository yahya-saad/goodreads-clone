using Goodreads.Application.Common.Responses;
using Goodreads.Application.DTOs;

namespace Goodreads.Application.Authors.Queries.GetAllAuthors;
public record GetAllAuthorsQuery(QueryParameters Parameters) : IRequest<PagedResult<AuthorDto>>;
