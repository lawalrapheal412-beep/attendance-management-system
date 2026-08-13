using MediatR;

namespace AttendanceManagementSystem.Application.Courses.Commands.DeleteCourse;

public sealed record DeleteCourseCommand(
    Guid Id) : IRequest<bool>;