using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Students.Commands.DeleteStudent;

public class DeleteStudentCommandHandler
    : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepository;

    public DeleteStudentCommandHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<bool> Handle(
        DeleteStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetByIdAsync(request.Id);

        if (student is null)
        {
            return false;
        }

        await _studentRepository.DeleteAsync(student);

        return true;
    }
}
