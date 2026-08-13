using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler
    : IRequestHandler<UpdateDepartmentCommand, bool>
{
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(
        UpdateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (department is null)
        {
            return false;
        }

        department.Update(request.Name, request.FacultyId);

        return await _departmentRepository.UpdateAsync(
            department,
            cancellationToken);
    }
}