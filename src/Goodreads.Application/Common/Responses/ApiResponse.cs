namespace Goodreads.Application.Common.Responses;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public List<string>? ErrorMessages { get; set; } = new();

    public static ApiResponse Success(string? message = null)
    {
        return new ApiResponse
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static ApiResponse Failure(List<string> errorMessages, string? message = null)
    {
        return new ApiResponse
        {
            IsSuccess = false,
            Message = message,
            ErrorMessages = errorMessages
        };
    }

    public static ApiResponse Failure(string errorMessage, string? message = null)
    {
        return new ApiResponse
        {
            IsSuccess = false,
            Message = message,
            ErrorMessages = [errorMessage]
        };
    }


}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }
}