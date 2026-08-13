using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.UpdateAttendanceRecordStatus;

public sealed record UpdateAttendanceRecordStatusCommand(
    Guid Id,
    AttendanceStatus Status) : IRequest<bool>;