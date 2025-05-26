namespace Goodreads.Application.Common.Responses;
public record QueryParameters(
    string? Query,
    string? SortColumn,
    string? SortOrder,
    int? PageNumber,
    int? PageSize
);