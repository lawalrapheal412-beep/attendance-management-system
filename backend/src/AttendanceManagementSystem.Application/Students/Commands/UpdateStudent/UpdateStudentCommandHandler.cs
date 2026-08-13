using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Commands.UpdateStudent;

public class UpdateStudentCommandHandler
    : IRequestHandler<UpdateStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepository;

    public UpdateStudentCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<bool> Handle(
        UpdateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id);

        if (student is null)
        {
            return false;
        }

        student.Update(
            request.MatricNumber,
            request.UserId,
            request.DepartmentId,
            request.DateOfBirth,
            request.Level);

        await _studentRepository.UpdateAsync(student);

        return true;
    }



}