using AttendanceManagementSystem.Application.AttendanceSessions.Commands.CreateAttendanceSession;
using AttendanceManagementSystem.Application.AttendanceSessions.Commands.DeleteAttendanceSession;
using AttendanceManagementSystem.Application.AttendanceSessions.Commands.UpdateAttendanceSessionStatus;
using AttendanceManagementSystem.Application.AttendanceSessions.Queries.GetAllAttendanceSessions;
using AttendanceManagementSystem.Application.AttendanceSessions.Queries.GetAttendanceSessionById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.AttendanceSessions;

public static class AttendanceSessionEndpoints
{
    public static IEndpointRouteBuilder MapAttendanceSessionEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/attendance-sessions")
            .WithTags("Attendance Sessions");

        group.MapGet("/", GetAllAttendanceSessions);

        group.MapGet("/{id:guid}", GetAttendanceSessionById);

        group.MapPost("/", CreateAttendanceSession);

        group.MapPut("/{id:guid}/status", UpdateAttendanceSessionStatus);

        group.MapDelete("/{id:guid}", DeleteAttendanceSession);

        return app;
    }

    private static async Task<IResult> GetAllAttendanceSessions(
        ISender sender)
    {
        var result = await sender.Send(
            new GetAllAttendanceSessionsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetAttendanceSessionById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetAttendanceSessionByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateAttendanceSession(
        CreateAttendanceSessionCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created(
            $"/api/attendance-sessions/{id}",
            id);
    }

    private static async Task<IResult> UpdateAttendanceSessionStatus(
        Guid id,
        UpdateAttendanceSessionStatusCommand command,
        ISender sender)
    {
        if (id != command.Id)
        {
            return Results.BadRequest(
                "Route ID and Command ID do not match.");
        }

        var success = await sender.Send(command);

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteAttendanceSession(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteAttendanceSessionCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}