using AttendanceManagementSystem.Application.AttendanceSessions.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Queries.GetAllAttendanceSessions;

public sealed record GetAllAttendanceSessionsQuery
    : IRequest<IEnumerable<AttendanceSessionDto>>;