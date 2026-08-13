using AttendanceManagementSystem.Application.Authentication.Commands.Login;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Authentication;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication");

        group.MapPost("/login", Login);

        return app;
    }
private static async Task<IResult> Login(
    LoginCommand command,
    ISender sender)
{
    var result = await sender.Send(command);

    if (result is null)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(result);
}
}