using AttendanceManagementSystem.Application.Users.Commands.CreateUser;
using AttendanceManagementSystem.Application.Users.Commands.DeleteUser;
using AttendanceManagementSystem.Application.Users.Commands.UpdateUser;
using AttendanceManagementSystem.Application.Users.Queries.GetAllUsers;
using AttendanceManagementSystem.Application.Users.Queries.GetUserById;
using AttendanceManagementSystem.Application.Users.Commands.ResendPasswordSetup;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Users;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users")
            .RequireAuthorization(policy =>
             policy.RequireRole("Admin"));

        group.MapGet("/", GetAllUsers);

        group.MapGet("/{id:guid}", GetUserById);

        group.MapPost("/", CreateUser);

        group.MapPut("/{id:guid}", UpdateUser);

        group.MapDelete("/{id:guid}", DeleteUser);

        group.MapPost(
    "/{id:guid}/resend-password-setup",
    ResendPasswordSetup);

        return app;
    }

    private static async Task<IResult> GetAllUsers(
        ISender sender)
    {
        var result = await sender.Send(new GetAllUsersQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetUserById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetUserByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateUser(
        CreateUserCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created($"/api/users/{id}", id);
    }

    private static async Task<IResult> UpdateUser(
        Guid id,
        UpdateUserCommand command,
        ISender sender)
    {
        if (id != command.Id)
        {
            return Results.BadRequest("Route ID and Command ID do not match.");
        }

        var success = await sender.Send(command);

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteUser(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteUserCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }

    private static async Task<IResult> ResendPasswordSetup(
    Guid id,
    ISender sender)
{
    var success = await sender.Send(
        new ResendPasswordSetupCommand(id));

    return success
        ? Results.NoContent()
        : Results.BadRequest(
            "User does not exist or already has a password.");
}
}