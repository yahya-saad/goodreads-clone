using Goodreads.Application.Common;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Goodreads.API.Common;

public static class CustomResults
{
    public static IActionResult Problem<T>(Result<T> result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot return Problem() on successful result.");

        var problemDetails = new ProblemDetails
        {
            Title = result.Error.Code,
            Detail = result.Error.Description,
            Status = GetStatusCode(result.Error.Type),
            Type = GetLink(result.Error.Type)
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }


    public static IActionResult Problem(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot return Problem() on successful result.");

        var problemDetails = new ProblemDetails
        {
            Title = result.Error.Code,
            Detail = result.Error.Description,
            Status = GetStatusCode(result.Error.Type),
            Type = GetLink(result.Error.Type)
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    /*
        public static IActionResult FromResult<T>(Result<T> result)
        {
            return result.Match(
                onSuccess: data => new OkObjectResult(data),
                onFailure: failure => Problem(failure)
            );
        }

        public static IActionResult FromResult(Result result)
        {
            return result.Match(
                onSuccess: () => new OkResult(),
                onFailure: failure => Problem(failure)
            );
        }
    */


    private static int GetStatusCode(ErrorType type) =>
        type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure or ErrorType.Validation or ErrorType.Problem => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetLink(ErrorType type) =>
        type switch
        {
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            ErrorType.Failure => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
            ErrorType.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
}
