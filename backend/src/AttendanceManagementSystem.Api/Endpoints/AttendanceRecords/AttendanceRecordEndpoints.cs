using AttendanceManagementSystem.Application.AttendanceRecords.Commands.CreateAttendanceRecord;
using AttendanceManagementSystem.Application.AttendanceRecords.Commands.DeleteAttendanceRecord;
using AttendanceManagementSystem.Application.AttendanceRecords.Commands.UpdateAttendanceRecordStatus;
using AttendanceManagementSystem.Application.AttendanceRecords.Queries.GetAllAttendanceRecords;
using AttendanceManagementSystem.Application.AttendanceRecords.Queries.GetAttendanceRecordById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.AttendanceRecords;

public static class AttendanceRecordEndpoints
{
    public static IEndpointRouteBuilder MapAttendanceRecordEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/attendance-records")
            .WithTags("Attendance Records");

        group.MapGet("/", GetAllAttendanceRecords);

        group.MapGet("/{id:guid}", GetAttendanceRecordById);

        group.MapPost("/", CreateAttendanceRecord);

        group.MapPut("/{id:guid}/status", UpdateAttendanceRecordStatus);

        group.MapDelete("/{id:guid}", DeleteAttendanceRecord);

        return app;
    }

    private static async Task<IResult> GetAllAttendanceRecords(
        ISender sender)
    {
        var result = await sender.Send(
            new GetAllAttendanceRecordsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetAttendanceRecordById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetAttendanceRecordByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateAttendanceRecord(
        CreateAttendanceRecordCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created(
            $"/api/attendance-records/{id}",
            id);
    }

    private static async Task<IResult> UpdateAttendanceRecordStatus(
        Guid id,
        UpdateAttendanceRecordStatusCommand command,
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

    private static async Task<IResult> DeleteAttendanceRecord(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteAttendanceRecordCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}