using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.CreateAcademicSession;

public sealed record CreateAcademicSessionCommand(
    string Name,
    bool IsCurrent)
    : IRequest<Guid>;