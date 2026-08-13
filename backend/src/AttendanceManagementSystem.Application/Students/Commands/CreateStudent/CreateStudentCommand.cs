using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Commands.CreateStudent;

public record CreateStudentCommand(
    string MatricNumber,
    string FullName,
    string Email,
    Level Level,
    Guid DepartmentId,
    DateOnly DateOfBirth
) : IRequest<Guid>;