using AttendanceManagementSystem.Application.AttendanceRecords.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Queries.GetAttendanceRecordById;

public sealed record GetAttendanceRecordByIdQuery(
    Guid Id) : IRequest<AttendanceRecordDto?>;