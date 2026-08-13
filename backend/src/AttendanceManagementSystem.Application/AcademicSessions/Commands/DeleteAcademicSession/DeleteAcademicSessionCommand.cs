using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.DeleteAcademicSession;

public sealed record DeleteAcademicSessionCommand(
    Guid Id) : IRequest<bool>;