using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.DeleteAttendanceSession;

public sealed class DeleteAttendanceSessionCommandHandler
    : IRequestHandler<DeleteAttendanceSessionCommand, bool>
{
    private readonly IAttendanceSessionRepository _repository;

    public DeleteAttendanceSessionCommandHandler(
        IAttendanceSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        DeleteAttendanceSessionCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(
            request.Id,
            cancellationToken);
    }
}