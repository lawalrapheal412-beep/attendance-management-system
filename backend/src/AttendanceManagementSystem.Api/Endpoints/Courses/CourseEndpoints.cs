using AttendanceManagementSystem.Application.Courses.Commands.CreateCourse;
using AttendanceManagementSystem.Application.Courses.Commands.DeleteCourse;
using AttendanceManagementSystem.Application.Courses.Commands.UpdateCourse;
using AttendanceManagementSystem.Application.Courses.Queries.GetAllCourses;
using AttendanceManagementSystem.Application.Courses.Queries.GetCourseById;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.Courses;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCourseEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/courses")
    .WithTags("Courses")
    .RequireAuthorization(policy =>
        policy.RequireRole("Admin"));

        group.MapGet("/", GetAllCourses);

        group.MapGet("/{id:guid}", GetCourseById);

        group.MapPost("/", CreateCourse);

        group.MapPut("/{id:guid}", UpdateCourse);

        group.MapDelete("/{id:guid}", DeleteCourse);

        return app;
    }

    private static async Task<IResult> GetAllCourses(
        ISender sender)
    {
        var result = await sender.Send(new GetAllCoursesQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetCourseById(
        Guid id,
        ISender sender)
    {
        var result = await sender.Send(
            new GetCourseByIdQuery(id));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> CreateCourse(
        CreateCourseCommand command,
        ISender sender)
    {
        var id = await sender.Send(command);

        return Results.Created($"/api/courses/{id}", id);
    }

    private static async Task<IResult> UpdateCourse(
        Guid id,
        UpdateCourseCommand command,
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

    private static async Task<IResult> DeleteCourse(
        Guid id,
        ISender sender)
    {
        var success = await sender.Send(
            new DeleteCourseCommand(id));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}