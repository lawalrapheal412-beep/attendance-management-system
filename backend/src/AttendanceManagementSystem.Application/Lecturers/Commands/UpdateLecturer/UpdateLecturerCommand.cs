using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.UpdateLecturer;

public sealed record UpdateLecturerCommand(
    Guid Id,
    Guid UserId,
    Guid DepartmentId
) : IRequest<bool>;