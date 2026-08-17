using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Departments.Commands.CreateDepartment;

public sealed class CreateDepartmentCommandHandler
    : IRequestHandler<CreateDepartmentCommand, Guid>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IFacultyRepository _facultyRepository;

    public CreateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository,
        IFacultyRepository facultyRepository)
    {
        _departmentRepository = departmentRepository;
        _facultyRepository = facultyRepository;
    }

    public async Task<Guid> Handle(
        CreateDepartmentCommand request,
        CancellationToken cancellationToken)
    {
        var faculty = await _facultyRepository.GetByIdAsync(
            request.FacultyId,
            cancellationToken);

        if (faculty is null)
        {
            throw new InvalidOperationException(
                "The specified faculty does not exist.");
        }

        var department = new Department(
            request.Name,
            request.FacultyId);

        await _departmentRepository.AddAsync(
            department,
            cancellationToken);

        return department.Id;
    }
}