using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Lecturers.Commands.UpdateLecturer;

public sealed class UpdateLecturerCommandHandler
    : IRequestHandler<UpdateLecturerCommand, bool>
{
    private readonly ILecturerRepository _lecturerRepository;

    public UpdateLecturerCommandHandler(
        ILecturerRepository lecturerRepository)
    {
        _lecturerRepository = lecturerRepository;
    }

    public async Task<bool> Handle(
        UpdateLecturerCommand request,
        CancellationToken cancellationToken)
    {
        var lecturer = await _lecturerRepository.GetByIdAsync(request.Id);

        if (lecturer is null)
        {
            return false;
        }

        lecturer.Update(
            request.UserId,
            request.DepartmentId);

        await _lecturerRepository.UpdateAsync(lecturer);

        return true;
    }
}