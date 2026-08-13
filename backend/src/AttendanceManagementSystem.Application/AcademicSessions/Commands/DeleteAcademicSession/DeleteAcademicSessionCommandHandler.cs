using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AcademicSessions.Commands.DeleteAcademicSession;

public class DeleteAcademicSessionCommandHandler
    : IRequestHandler<DeleteAcademicSessionCommand, bool>
{
    private readonly IAcademicSessionRepository _repository;

    public DeleteAcademicSessionCommandHandler(
        IAcademicSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteAcademicSessionCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}