using AttendanceManagementSystem.Application.AcademicSessions.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Queries.GetAcademicSessionById;

public sealed record GetAcademicSessionByIdQuery(
    Guid Id) : IRequest<AcademicSessionDto?>;