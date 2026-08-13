using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Departments.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Queries.GetDepartmentById;

public class GetDepartmentByIdQueryHandler
    : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto?>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentByIdQueryHandler(
        IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDto?> Handle(
        GetDepartmentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (department is null)
        {
            return null;
        }

        return new DepartmentDto(
            department.Id,
            department.Name,
            department.FacultyId);
    }
}