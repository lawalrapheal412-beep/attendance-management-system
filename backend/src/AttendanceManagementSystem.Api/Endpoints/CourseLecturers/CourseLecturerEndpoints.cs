using AttendanceManagementSystem.Application.CourseLecturers.Commands.AssignLecturerToCourse;
using AttendanceManagementSystem.Application.CourseLecturers.Commands.RemoveLecturerFromCourse;
using AttendanceManagementSystem.Application.CourseLecturers.Queries.GetAllCourseLecturers;
using AttendanceManagementSystem.Application.CourseLecturers.Queries.GetCourseLecturer;
using AttendanceManagementSystem.Application.CourseLecturers.Queries.GetCoursesByLecturer;
using AttendanceManagementSystem.Application.CourseLecturers.Queries.GetLecturersByCourse;
using MediatR;

namespace AttendanceManagementSystem.Api.Endpoints.CourseLecturers;

public static class CourseLecturerEndpoints
{
    public static IEndpointRouteBuilder MapCourseLecturerEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/...")
    .RequireAuthorization(policy =>
        policy.RequireRole("Admin"));

        group.MapGet("/", GetAllCourseLecturers);

        group.MapGet(
            "/{courseId:guid}/{lecturerId:guid}",
            GetCourseLecturer);

        group.MapGet(
            "/course/{courseId:guid}",
            GetLecturersByCourse);

        group.MapGet(
            "/lecturer/{lecturerId:guid}",
            GetCoursesByLecturer);

        group.MapPost(
            "/assign",
            AssignLecturerToCourse);

        group.MapDelete(
            "/{courseId:guid}/{lecturerId:guid}",
            RemoveLecturerFromCourse);

        return app;
    }

    private static async Task<IResult> GetAllCourseLecturers(
        ISender sender)
    {
        var result = await sender.Send(
            new GetAllCourseLecturersQuery());

        return Results.Ok(result);
    }

    private static async Task<IResult> GetCourseLecturer(
        Guid courseId,
        Guid lecturerId,
        ISender sender)
    {
        var result = await sender.Send(
            new GetCourseLecturerQuery(
                courseId,
                lecturerId));

        return result is null
            ? Results.NotFound()
            : Results.Ok(result);
    }

    private static async Task<IResult> GetLecturersByCourse(
        Guid courseId,
        ISender sender)
    {
        var result = await sender.Send(
            new GetLecturersByCourseQuery(courseId));

        return Results.Ok(result);
    }

    private static async Task<IResult> GetCoursesByLecturer(
        Guid lecturerId,
        ISender sender)
    {
        var result = await sender.Send(
            new GetCoursesByLecturerQuery(lecturerId));

        return Results.Ok(result);
    }

    private static async Task<IResult> AssignLecturerToCourse(
        AssignLecturerToCourseCommand command,
        ISender sender)
    {
        var success = await sender.Send(command);

        return success
            ? Results.Created(
                $"/api/course-lecturers/{command.CourseId}/{command.LecturerId}",
                null)
            : Results.Conflict(
                "The lecturer is already assigned to this course.");
    }

    private static async Task<IResult> RemoveLecturerFromCourse(
        Guid courseId,
        Guid lecturerId,
        ISender sender)
    {
        var success = await sender.Send(
            new RemoveLecturerFromCourseCommand(
                courseId,
                lecturerId));

        return success
            ? Results.NoContent()
            : Results.NotFound();
    }
}