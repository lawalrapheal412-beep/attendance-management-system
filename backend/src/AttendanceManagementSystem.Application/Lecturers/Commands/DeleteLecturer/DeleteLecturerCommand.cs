using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.DeleteLecturer;

public sealed record DeleteLecturerCommand(
    Guid Id
) : IRequest<bool>;