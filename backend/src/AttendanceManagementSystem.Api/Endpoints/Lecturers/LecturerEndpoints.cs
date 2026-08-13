using AttendanceManagementSystem.Application.Lecturers.Commands.CreateLecturer;
using AttendanceManagementSystem.Application.Lecturers.Commands.DeleteLecturer;
using AttendanceManagementSystem.Application.Lecturers.Commands.UpdateLecturer;
using AttendanceManagementSystem.Application.Lecturers.Queries.GetAllLecturers;
using AttendanceManagementSystem.Application.Lecturers.Queries.GetLecturerById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Lecturers;

public static class LecturerEndpoints
{
    public static IEndpointRouteBuilder MapLecturerEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/lecturers")
            .WithTags("Lecturers");

        group.MapGet("/", GetAllLecturers);

        group.MapGet("/{id:guid}", GetLecturerById);

        group.MapPost("/", CreateLecturer);

        group.MapPut("/{id:guid}", UpdateLecturer);

        group.MapDelete("/{id:guid}", DeleteLecturer);

        return app;
    }

    private static async Task<IResult> GetAllLecturers(
        ISender sender)
    {
        var result = await sender.Send(new GetAllLecturersQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetLecturerById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetLecturerByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateLecturer(
        CreateLecturerCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created($"/api/lecturers/{id}", id);
    }

    private static async Task<IResult> UpdateLecturer(
        Guid id,
        UpdateLecturerCommand command,
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

    private static async Task<IResult> DeleteLecturer(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteLecturerCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}