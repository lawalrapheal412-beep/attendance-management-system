using AttendanceManagementSystem.Application.Departments.Commands.CreateDepartment;
using AttendanceManagementSystem.Application.Departments.Commands.DeleteDepartment;
using AttendanceManagementSystem.Application.Departments.Commands.UpdateDepartment;
using AttendanceManagementSystem.Application.Departments.Queries.GetAllDepartments;
using AttendanceManagementSystem.Application.Departments.Queries.GetDepartmentById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Departments;

public static class DepartmentEndpoints
{
    public static IEndpointRouteBuilder MapDepartmentEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/departments")
            .WithTags("Departments");

        group.MapGet("/", GetAllDepartments);

        group.MapGet("/{id:guid}", GetDepartmentById);

        group.MapPost("/", CreateDepartment);

        group.MapPut("/{id:guid}", UpdateDepartment);

        group.MapDelete("/{id:guid}", DeleteDepartment);

        return app;
    }

    private static async Task<IResult> GetAllDepartments(
        ISender sender)
    {
        var result = await sender.Send(
            new GetAllDepartmentsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetDepartmentById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetDepartmentByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateDepartment(
        CreateDepartmentCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created(
            $"/api/departments/{id}",
            id);
    }

    private static async Task<IResult> UpdateDepartment(
        Guid id,
        UpdateDepartmentCommand command,
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

    private static async Task<IResult> DeleteDepartment(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteDepartmentCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}