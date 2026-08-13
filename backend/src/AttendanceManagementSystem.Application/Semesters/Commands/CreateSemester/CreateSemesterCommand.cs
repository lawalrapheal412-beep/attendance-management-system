using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Commands.CreateSemester;

public sealed record CreateSemesterCommand(
    Guid AcademicSessionId,
    string Name )
    : IRequest<Guid>;