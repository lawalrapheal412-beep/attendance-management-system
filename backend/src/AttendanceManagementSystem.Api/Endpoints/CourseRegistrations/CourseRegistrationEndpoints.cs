using AttendanceManagementSystem.Application.CourseRegistrations.Commands.CreateCourseRegistration;
using AttendanceManagementSystem.Application.CourseRegistrations.Commands.UpdateCourseRegistration;
using AttendanceManagementSystem.Application.CourseRegistrations.Commands.DeleteCourseRegistration;
using AttendanceManagementSystem.Application.CourseRegistrations.Queries.GetAllCourseRegistrations;
using AttendanceManagementSystem.Application.CourseRegistrations.Queries.GetCourseRegistrationById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.CourseRegistrations;

public static class CourseRegistrationEndpoints
{
    public static IEndpointRouteBuilder MapCourseRegistrationEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/course-registrations")
    .RequireAuthorization();

        group.MapGet("/", GetAllCourseRegistrations);

        group.MapGet("/{id:guid}", GetCourseRegistrationById);

        group.MapPost("/", CreateCourseRegistration);

        group.MapPut("/{id:guid}", UpdateCourseRegistration);

        group.MapDelete("/{id:guid}", DeleteCourseRegistration);

        return app;
    }

    private static async Task<IResult> GetAllCourseRegistrations(
        ISender sender)
    {
        var result = await sender.Send(new GetAllCourseRegistrationsQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetCourseRegistrationById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetCourseRegistrationByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateCourseRegistration(
        CreateCourseRegistrationCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created(
            $"/api/course-registrations/{id}",
            id);
    }

    private static async Task<IResult> UpdateCourseRegistration(
        Guid id,
        UpdateCourseRegistrationCommand command,
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

    private static async Task<IResult> DeleteCourseRegistration(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteCourseRegistrationCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}