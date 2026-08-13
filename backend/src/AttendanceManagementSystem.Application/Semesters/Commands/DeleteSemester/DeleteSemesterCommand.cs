using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Commands.DeleteSemester;

public sealed record DeleteSemesterCommand(Guid Id)
    : IRequest<bool>;