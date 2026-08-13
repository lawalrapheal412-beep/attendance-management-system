using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Commands.UpdateSemester;

public sealed record UpdateSemesterCommand(
    Guid Id,
    string Name,
    Guid AcademicSessionId)
    : IRequest<bool>;