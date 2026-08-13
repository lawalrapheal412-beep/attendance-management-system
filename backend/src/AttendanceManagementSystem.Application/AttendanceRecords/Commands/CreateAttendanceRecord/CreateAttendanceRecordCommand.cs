using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.CreateAttendanceRecord;

public sealed record CreateAttendanceRecordCommand(
    Guid AttendanceSessionId,
    Guid StudentId,
    AttendanceStatus Status) : IRequest<Guid>;