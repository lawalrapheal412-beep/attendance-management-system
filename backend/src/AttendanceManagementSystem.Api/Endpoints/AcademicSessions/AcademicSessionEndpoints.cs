using AttendanceManagementSystem.Application.AcademicSessions.Commands.CreateAcademicSession;
using AttendanceManagementSystem.Application.AcademicSessions.Commands.DeleteAcademicSession;
using AttendanceManagementSystem.Application.AcademicSessions.Commands.UpdateAcademicSession;
using AttendanceManagementSystem.Application.AcademicSessions.Queries.GetAcademicSessionById;
using AttendanceManagementSystem.Application.AcademicSessions.Queries.GetAllAcademicSessions;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.AcademicSessions;

public static class AcademicSessionEndpoints
{
    public static IEndpointRouteBuilder MapAcademicSessionEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/academic-sessions")
            .WithTags("Academic Sessions");

        group.MapGet("/", GetAllAcademicSessions);

        group.MapGet("/{id:guid}", GetAcademicSessionById);

        group.MapPost("/", CreateAcademicSession);

        group.MapPut("/{id:guid}", UpdateAcademicSession);

        group.MapDelete("/{id:guid}", DeleteAcademicSession);

        return app;
    }

    private static async Task<IResult> GetAllAcademicSessions(
        ISender sender)
    {
        var result = await sender.Send(
            new GetAllAcademicSessionsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetAcademicSessionById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetAcademicSessionByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateAcademicSession(
        CreateAcademicSessionCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created(
            $"/api/academic-sessions/{id}",
            id);
    }

    private static async Task<IResult> UpdateAcademicSession(
        Guid id,
        UpdateAcademicSessionCommand command,
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

    private static async Task<IResult> DeleteAcademicSession(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteAcademicSessionCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}