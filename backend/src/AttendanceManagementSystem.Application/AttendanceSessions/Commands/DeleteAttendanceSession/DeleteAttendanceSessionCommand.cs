using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.DeleteAttendanceSession;

public sealed record DeleteAttendanceSessionCommand(
    Guid Id) : IRequest<bool>;