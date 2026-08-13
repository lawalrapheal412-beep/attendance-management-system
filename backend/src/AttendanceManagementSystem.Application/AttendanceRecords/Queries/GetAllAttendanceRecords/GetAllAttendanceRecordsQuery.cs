using AttendanceManagementSystem.Application.AttendanceRecords.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceRecords.Queries.GetAllAttendanceRecords;

public sealed record GetAllAttendanceRecordsQuery
    : IRequest<IEnumerable<AttendanceRecordDto>>;