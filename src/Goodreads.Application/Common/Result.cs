namespace Goodreads.Application.Common;
public class Result<T>
{
    public bool Success { get; }
    public T? Data { get; }
    public string? ErrorMessage { get; }

    public Result(bool success, T? data, string? errorMessage)
    {
        Success = success;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Ok(T data) => new(true, data, null);
    public static Result<T> Fail(string errorMessage) => new(false, default, errorMessage);
}
