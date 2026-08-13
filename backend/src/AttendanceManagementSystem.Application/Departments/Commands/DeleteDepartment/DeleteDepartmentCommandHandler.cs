using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler
    : IRequestHandler<DeleteDepartmentCommand, bool>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteDepartmentCommandHandler(
        IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(
        DeleteDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (department is null)
        {
            return false;
        }

        return await _departmentRepository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}