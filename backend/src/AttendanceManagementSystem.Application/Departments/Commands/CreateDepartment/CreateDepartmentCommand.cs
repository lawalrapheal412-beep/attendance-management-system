using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Commands.CreateDepartment;

public sealed record CreateDepartmentCommand(
    string Name,
    Guid FacultyId
) : IRequest<Guid>;