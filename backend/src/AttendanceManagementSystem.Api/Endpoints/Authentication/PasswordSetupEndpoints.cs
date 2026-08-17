using AttendanceManagementSystem.Application.Users.Commands.SetPassword;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Authentication;

public static class PasswordSetupEndpoints
{
    public static IEndpointRouteBuilder MapPasswordSetupEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication");

        group.MapPost("/set-password", SetPassword);

        return app;
    }

    private static async Task<IResult> SetPassword(
        SetPasswordCommand command,
        ISender sender)
    {
        var success = await sender.Send(command);

        return success
            ? Results.NoContent()
            : Results.BadRequest(
                "The password setup token is invalid or expired.");
    }
}