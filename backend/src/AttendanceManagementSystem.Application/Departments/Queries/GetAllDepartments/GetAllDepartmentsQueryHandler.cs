using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Application.Departments.DTOs;
using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Queries.GetAllDepartments;

public sealed class GetAllDepartmentsQueryHandler
    : IRequestHandler<GetAllDepartmentsQuery, IEnumerable<DepartmentDto>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetAllDepartmentsQueryHandler(
        IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<DepartmentDto>> Handle(
        GetAllDepartmentsQuery request,
         CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);

        return departments.Select(department => new DepartmentDto(
            department.Id,
            department.Name,
            department.FacultyId
        ));
    }
}