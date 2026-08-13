using MediatR;
using AttendanceManagementSystem.Application.Departments.DTOs;

namespace AttendanceManagementSystem.Application.Departments.Queries.GetDepartmentById;

public sealed record GetDepartmentByIdQuery(Guid Id)
    : IRequest<DepartmentDto?>;