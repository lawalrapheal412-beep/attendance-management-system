using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Commands.UpdateStudent;

public sealed record UpdateStudentCommand(
    Guid Id,
    string MatricNumber,
    Guid UserId,
    Guid DepartmentId,
    DateOnly DateOfBirth,
    Level Level
) : IRequest<bool>;