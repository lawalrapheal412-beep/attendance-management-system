using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Commands.UpdateCourse;

public sealed record UpdateCourseCommand(
    Guid Id,
    string Code,
    string Title,
    int Units,
    Guid DepartmentId) : IRequest<bool>;