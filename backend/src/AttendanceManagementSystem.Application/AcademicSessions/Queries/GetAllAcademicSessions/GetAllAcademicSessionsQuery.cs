using AttendanceManagementSystem.Application.AcademicSessions.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Queries.GetAllAcademicSessions;

public sealed record GetAllAcademicSessionsQuery
    : IRequest<IEnumerable<AcademicSessionDto>>;