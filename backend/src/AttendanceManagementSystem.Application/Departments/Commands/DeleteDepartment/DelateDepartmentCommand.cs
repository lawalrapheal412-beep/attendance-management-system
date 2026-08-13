using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Commands.DeleteDepartment;

public sealed record DeleteDepartmentCommand(
    Guid Id) : IRequest<bool>;