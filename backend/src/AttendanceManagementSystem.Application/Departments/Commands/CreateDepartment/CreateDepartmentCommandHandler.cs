using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler
    : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Guid> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var department = new Department(
            request.Name,
            Guid.NewGuid());

        await _departmentRepository.AddAsync(
            department,
            cancellationToken);

        return department.Id;
    }
}