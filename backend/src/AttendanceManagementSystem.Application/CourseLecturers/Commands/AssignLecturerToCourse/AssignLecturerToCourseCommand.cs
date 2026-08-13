using MediatR;

namespace AttendanceManagementSystem.Application.CourseLecturers.Commands.AssignLecturerToCourse;

public sealed record AssignLecturerToCourseCommand(
    Guid CourseId,
    Guid LecturerId) : IRequest<bool>;