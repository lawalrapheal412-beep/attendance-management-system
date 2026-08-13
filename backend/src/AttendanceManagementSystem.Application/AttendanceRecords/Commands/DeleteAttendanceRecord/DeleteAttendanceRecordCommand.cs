using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Commands.DeleteAttendanceRecord;

public sealed record DeleteAttendanceRecordCommand(
    Guid Id) : IRequest<bool>;