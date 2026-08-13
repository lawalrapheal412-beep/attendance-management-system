using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler
    : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseCommandHandler(
        ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Guid> Handle(
        CreateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var course = new Course(
            request.Code,
            request.Title,
            request.Units,
            request.DepartmentId);

        await _courseRepository.AddAsync(
            course,
            cancellationToken);

        return course.Id;
    }
}