using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.UpdateAcademicSession;

public class UpdateAcademicSessionCommandHandler
    : IRequestHandler<UpdateAcademicSessionCommand, bool>
{
    private readonly IAcademicSessionRepository _repository;

    public UpdateAcademicSessionCommandHandler(
        IAcademicSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateAcademicSessionCommand request,
        CancellationToken cancellationToken)
    {
        var academicSession = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (academicSession is null)
        {
            return false;
        }

        academicSession.Update(
            request.Name,
            request.IsCurrent);

        return await _repository.UpdateAsync(
            academicSession,
            cancellationToken);
    }
}