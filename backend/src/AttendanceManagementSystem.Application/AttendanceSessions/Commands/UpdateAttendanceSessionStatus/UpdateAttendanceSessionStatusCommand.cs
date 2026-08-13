using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.UpdateAttendanceSessionStatus;

public sealed record UpdateAttendanceSessionStatusCommand(
    Guid Id,
    bool IsOpen) : IRequest<bool>;