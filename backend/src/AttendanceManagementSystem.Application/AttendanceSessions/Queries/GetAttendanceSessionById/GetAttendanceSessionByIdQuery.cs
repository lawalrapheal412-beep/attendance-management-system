using AttendanceManagementSystem.Application.AttendanceSessions.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Queries.GetAttendanceSessionById;

public sealed record GetAttendanceSessionByIdQuery(
    Guid Id) : IRequest<AttendanceSessionDto?>;