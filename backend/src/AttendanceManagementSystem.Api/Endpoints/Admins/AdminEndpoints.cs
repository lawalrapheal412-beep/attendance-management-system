using AttendanceManagementSystem.Application.Admins.Commands.CreateAdmin;
using AttendanceManagementSystem.Application.Admins.Commands.DeleteAdmin;
using AttendanceManagementSystem.Application.Admins.Queries.GetAdminById;
using AttendanceManagementSystem.Application.Admins.Queries.GetAllAdmins;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Admins;

public static class AdminEndpoints
{
    public static IEndpointRouteBuilder MapAdminEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/...")
    .RequireAuthorization(policy =>
        policy.RequireRole("Admin"));

        group.MapGet("/", GetAllAdmins);

        group.MapGet("/{id:guid}", GetAdminById);

        group.MapPost("/", CreateAdmin);

        group.MapDelete("/{id:guid}", DeleteAdmin);

        return app;
    }

    private static async Task<IResult> GetAllAdmins(
        ISender sender)
    {
        var result = await sender.Send(
            new GetAllAdminsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetAdminById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetAdminByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateAdmin(
        CreateAdminCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created(
            $"/api/admins/{id}",
            id);
    }

    private static async Task<IResult> DeleteAdmin(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteAdminCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}