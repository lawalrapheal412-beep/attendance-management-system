using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using AttendanceManagementSystem.Domain.Enums;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Commands.CreateStudent;

public sealed class CreateStudentCommandHandler
    : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;

    public CreateStudentCommandHandler(
        IStudentRepository studentRepository,
        IUserRepository userRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(
        CreateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var existingStudent =
            await _studentRepository.GetByMatricNumberAsync(
                request.MatricNumber);

        if (existingStudent is not null)
        {
            throw new InvalidOperationException(
                "A student with this matric number already exists.");
        }

        var user =
            await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            throw new InvalidOperationException(
                "A user with this email does not exist.");
        }

        if (user.Role != UserRole.Student)
        {
            throw new InvalidOperationException(
                "The specified user is not a Student.");
        }

        var student = Student.Create(
            user.Id,
            request.DepartmentId,
            request.MatricNumber,
            request.Level,
            request.DateOfBirth);

        await _studentRepository.AddAsync(student);

        return student.Id;
    }
}