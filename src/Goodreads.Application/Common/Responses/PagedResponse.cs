namespace Goodreads.Application.Common.Responses;
public class PagedResponse<T> : ApiResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }

    public static PagedResponse<T> Create(T data, int pageNumber, int pageSize, int totalRecords, string? message = null)
    {
        return new PagedResponse<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }
}