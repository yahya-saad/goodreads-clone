using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Goodreads.API.Middlewares;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Internal Server Error",
            Detail = "An error occurred while processing your request.",
        };
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var context = new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails,
        };

        logger.LogError("An unhandled exception occurred: {Message}", exception.Message);

        return await problemDetailsService.TryWriteAsync(context);
    }
}
