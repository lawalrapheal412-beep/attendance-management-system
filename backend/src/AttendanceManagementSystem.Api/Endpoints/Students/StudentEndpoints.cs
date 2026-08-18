using AttendanceManagementSystem.Application.Students.Commands.CreateStudent;
using AttendanceManagementSystem.Application.Students.Commands.DeleteStudent;
using AttendanceManagementSystem.Application.Students.Commands.UpdateStudent;
using AttendanceManagementSystem.Application.Students.Queries.GetAllStudents;
using AttendanceManagementSystem.Application.Students.Queries.GetStudentById;
using AttendanceManagementSystem.Application.Students.Queries.GetStudentQrCode;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Students;

public static class StudentEndpoints
{
    public static IEndpointRouteBuilder MapStudentEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/students")
    .WithTags("Students")
    .RequireAuthorization(policy =>
        policy.RequireRole("Admin"));
        group.MapGet("/", GetAllStudents);

        group.MapGet("/{id:guid}", GetStudentById);

        group.MapPost("/", CreateStudent);

        group.MapPut("/{id:guid}", UpdateStudent);

        group.MapDelete("/{id:guid}", DeleteStudent);

        group.MapGet("/{id:guid}/qr-code", GetStudentQrCode);

        return app;
    }

    private static async Task<IResult> GetAllStudents(
        ISender sender)
    {
        var result = await sender.Send(new GetAllStudentsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetStudentById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetStudentByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateStudent(
        CreateStudentCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created($"/api/students/{id}", id);
    }

    private static async Task<IResult> UpdateStudent(
        Guid id,
        UpdateStudentCommand command,
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

    private static async Task<IResult> DeleteStudent(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteStudentCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }

    private static async Task<IResult> GetStudentQrCode(
    Guid id,
    ISender sender)
{
    var qrCode = await sender.Send(
        new GetStudentQrCodeQuery(id));

    return qrCode is null
        ? Results.NotFound()
        : Results.File(
            qrCode,
            "image/png",
            $"student-{id}-qr.png");
}
}