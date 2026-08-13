using AttendanceManagementSystem.Application.Departments.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Queries.GetAllDepartments;

public sealed record GetAllDepartmentsQuery
    : IRequest<IEnumerable<DepartmentDto>>;