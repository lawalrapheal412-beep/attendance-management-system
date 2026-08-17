using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Commands.CreateCourse;

public sealed class CreateCourseCommandHandler
    : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public CreateCourseCommandHandler(
        ICourseRepository courseRepository,
        IDepartmentRepository departmentRepository)
    {
        _courseRepository = courseRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<Guid> Handle(
        CreateCourseCommand request,
        CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(
            request.DepartmentId,
            cancellationToken);

        if (department is null)
        {
            throw new InvalidOperationException(
                "The specified department does not exist.");
        }

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