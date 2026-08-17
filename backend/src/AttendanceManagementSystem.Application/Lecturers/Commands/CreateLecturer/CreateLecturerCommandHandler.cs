using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.CreateLecturer;

public sealed class CreateLecturerCommandHandler
    : IRequestHandler<CreateLecturerCommand, Guid>
{
    private readonly ILecturerRepository _lecturerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public CreateLecturerCommandHandler(
        ILecturerRepository lecturerRepository,
        IUserRepository userRepository,
        IDepartmentRepository departmentRepository)
    {
        _lecturerRepository = lecturerRepository;
        _userRepository = userRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<Guid> Handle(
        CreateLecturerCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new InvalidOperationException(
                "The specified user does not exist.");
        }

        if (user.Role != UserRole.Lecturer)
        {
            throw new InvalidOperationException(
                "The specified user is not a Lecturer.");
        }

        var department = await _departmentRepository.GetByIdAsync(
            request.DepartmentId,
            cancellationToken);

        if (department is null)
        {
            throw new InvalidOperationException(
                "The specified department does not exist.");
        }

        var lecturer = Lecturer.Create(
            request.UserId,
            request.DepartmentId);

        await _lecturerRepository.AddAsync(lecturer);

        return lecturer.Id;
    }
}