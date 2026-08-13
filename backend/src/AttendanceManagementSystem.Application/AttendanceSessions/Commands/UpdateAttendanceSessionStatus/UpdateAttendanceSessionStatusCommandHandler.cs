using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.UpdateAttendanceSessionStatus;

public sealed class UpdateAttendanceSessionStatusCommandHandler
    : IRequestHandler<UpdateAttendanceSessionStatusCommand, bool>
{
    private readonly IAttendanceSessionRepository _repository;

    public UpdateAttendanceSessionStatusCommandHandler(
        IAttendanceSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateAttendanceSessionStatusCommand request,
        CancellationToken cancellationToken)
    {
        var attendanceSession = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (attendanceSession is null)
        {
            return false;
        }

        if (request.IsOpen)
        {
            attendanceSession.Open();
        }
        else
        {
            attendanceSession.Close();
        }

        return await _repository.UpdateAsync(
            attendanceSession,
            cancellationToken);
    }
}