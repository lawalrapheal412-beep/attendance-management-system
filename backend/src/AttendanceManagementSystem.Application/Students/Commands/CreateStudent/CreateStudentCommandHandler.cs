using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Commands.CreateStudent;

public class CreateStudentCommandHandler
    : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Guid> Handle(
        CreateStudentCommand request,
        CancellationToken cancellationToken)
    {
        // Check whether the matric number already exists
        var existingStudent =
            await _studentRepository.GetByMatricNumberAsync(request.MatricNumber);

        if (existingStudent != null)
        {
            throw new Exception("Student with this matric number already exists.");
        }

        // Create the student
        var student = Student.Create(
            Guid.NewGuid(),  // Temporary UserId
            request.DepartmentId,
            request.MatricNumber,
            request.Level,
            request.DateOfBirth);
        // Save the student
        await _studentRepository.AddAsync(student);
        // Return the generated Id
        return student.Id;
    }
}