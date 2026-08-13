using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.Semesters.Commands.UpdateSemester;

public sealed class UpdateSemesterCommandHandler
    : IRequestHandler<UpdateSemesterCommand, bool>
{
    private readonly ISemesterRepository _repository;

    public UpdateSemesterCommandHandler(
        ISemesterRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateSemesterCommand request,
        CancellationToken cancellationToken)
    {
        var semester = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (semester is null)
        {
            return false;
        }

        semester.Update(
            request.Name,
            request.AcademicSessionId);

        return await _repository.UpdateAsync(
            semester,
            cancellationToken);
    }
}