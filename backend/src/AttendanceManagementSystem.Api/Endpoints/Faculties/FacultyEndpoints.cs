using AttendanceManagementSystem.Application.Faculties.Commands.CreateFaculty;
using AttendanceManagementSystem.Application.Faculties.Commands.DeleteFaculty;
using AttendanceManagementSystem.Application.Faculties.Commands.UpdateFaculty;
using AttendanceManagementSystem.Application.Faculties.Queries.GetAllFaculties;
using AttendanceManagementSystem.Application.Faculties.Queries.GetFacultyById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Faculties;

public static class FacultyEndpoints
{
    public static IEndpointRouteBuilder MapFacultyEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/faculties")
            .WithTags("Faculties");

        group.MapGet("/", GetAllFaculties);

        group.MapGet("/{id:guid}", GetFacultyById);

        group.MapPost("/", CreateFaculty);

        group.MapPut("/{id:guid}", UpdateFaculty);

        group.MapDelete("/{id:guid}", DeleteFaculty);

        return app;
    }

    private static async Task<IResult> GetAllFaculties(
        ISender sender)
    {
        var result = await sender.Send(
            new GetAllFacultiesQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetFacultyById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetFacultyByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateFaculty(
        CreateFacultyCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created(
            $"/api/faculties/{id}",
            id);
    }

    private static async Task<IResult> UpdateFaculty(
        Guid id,
        UpdateFacultyCommand command,
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

    private static async Task<IResult> DeleteFaculty(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteFacultyCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}