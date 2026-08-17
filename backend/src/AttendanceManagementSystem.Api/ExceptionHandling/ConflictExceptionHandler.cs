using Microsoft.AspNetCore.Diagnostics;

namespace AttendanceManagementSystem.Api.ExceptionHandling;

public sealed class ConflictExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not InvalidOperationException)
        {
            return false;
        }

        if (!exception.Message.Contains(
                "already exists",
                StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        httpContext.Response.StatusCode =
            StatusCodes.Status409Conflict;

        await httpContext.Response.WriteAsJsonAsync(
            new
            {
                error = exception.Message
            },
            cancellationToken);

        return true;
    }
}