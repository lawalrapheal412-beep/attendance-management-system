using AttendanceManagementSystem.Application.Semesters.Commands.CreateSemester;
using AttendanceManagementSystem.Application.Semesters.Commands.DeleteSemester;
using AttendanceManagementSystem.Application.Semesters.Commands.UpdateSemester;
using AttendanceManagementSystem.Application.Semesters.Queries.GetAllSemesters;
using AttendanceManagementSystem.Application.Semesters.Queries.GetSemesterById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Semesters;

public static class SemesterEndpoints
{
    public static IEndpointRouteBuilder MapSemesterEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/semesters")
            .WithTags("Semesters");

        group.MapGet("/", GetAllSemesters);

        group.MapGet("/{id:guid}", GetSemesterById);

        group.MapPost("/", CreateSemester);

        group.MapPut("/{id:guid}", UpdateSemester);

        group.MapDelete("/{id:guid}", DeleteSemester);

        return app;
    }

    private static async Task<IResult> GetAllSemesters(
        ISender sender)
    {
        var result = await sender.Send(new GetAllSemestersQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetSemesterById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetSemesterByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateSemester(
        CreateSemesterCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created($"/api/semesters/{id}", id);
    }

    private static async Task<IResult> UpdateSemester(
        Guid id,
        UpdateSemesterCommand command,
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

    private static async Task<IResult> DeleteSemester(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteSemesterCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}