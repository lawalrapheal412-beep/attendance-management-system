using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Courses.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Queries.GetCourseById;

public class GetCourseByIdQueryHandler
    : IRequestHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdQueryHandler(
        ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseDto?> Handle(
        GetCourseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (course is null)
        {
            return null;
        }

        return new CourseDto(
            course.Id,
            course.Code,
            course.Title,
            course.Units,
            course.DepartmentId);
    }
}