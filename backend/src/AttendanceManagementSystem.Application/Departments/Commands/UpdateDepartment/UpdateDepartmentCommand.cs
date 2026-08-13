using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Commands.UpdateDepartment;

public sealed record UpdateDepartmentCommand(
    Guid Id,
    string Name,
    Guid FacultyId
    ) : IRequest<bool>;