using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.CreateLecturer;

public sealed record CreateLecturerCommand(
    Guid UserId,
    Guid DepartmentId
) : IRequest<Guid>;