using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Courses.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Queries.GetAllCourses;

public sealed class GetAllCoursesQueryHandler
    : IRequestHandler<GetAllCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly ICourseRepository _courseRepository;

    public GetAllCoursesQueryHandler(
        ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<CourseDto>> Handle(
        GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);

        return courses.Select(course => new CourseDto(
            course.Id,
            course.Code,
            course.Title,
            course.Units,
            course.DepartmentId
        ));
    }
}