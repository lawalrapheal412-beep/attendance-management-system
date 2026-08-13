using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.UpdateAcademicSession;

public sealed record UpdateAcademicSessionCommand(
    Guid Id,
    string Name,
    bool IsCurrent) : IRequest<bool>;