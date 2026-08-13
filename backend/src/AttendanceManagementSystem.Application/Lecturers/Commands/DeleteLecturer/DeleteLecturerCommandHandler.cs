using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.DeleteLecturer;

public sealed class DeleteLecturerCommandHandler
    : IRequestHandler<DeleteLecturerCommand, bool>
{
    private readonly ILecturerRepository _lecturerRepository;

    public DeleteLecturerCommandHandler(
        ILecturerRepository lecturerRepository)
    {
        _lecturerRepository = lecturerRepository;
    }

    public async Task<bool> Handle(
        DeleteLecturerCommand request,
        CancellationToken cancellationToken)
    {
        var lecturer = await _lecturerRepository.GetByIdAsync(request.Id);

        if (lecturer is null)
        {
            return false;
        }

        await _lecturerRepository.DeleteAsync(lecturer);

        return true;
    }
}