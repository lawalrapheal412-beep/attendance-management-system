using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Commands.CreateCourse;

public sealed record CreateCourseCommand(
    string Code,
    string Title,
    int Units,
    Guid DepartmentId) : IRequest<Guid>;