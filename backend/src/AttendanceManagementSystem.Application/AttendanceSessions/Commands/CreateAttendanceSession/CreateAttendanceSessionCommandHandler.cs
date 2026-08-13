using AttendanceManagementSystem.Application.Common.Interfaces;
using AttendanceManagementSystem.Domain.Entities;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.CreateAttendanceSession;

public sealed class CreateAttendanceSessionCommandHandler
    : IRequestHandler<CreateAttendanceSessionCommand, Guid>
{
    private readonly IAttendanceSessionRepository _repository;

    public CreateAttendanceSessionCommandHandler(
        IAttendanceSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateAttendanceSessionCommand request,
        CancellationToken cancellationToken)
    {
        var attendanceSession = new AttendanceSession(
            request.CourseId,
            request.LecturerId,
            request.SemesterId,
            request.AcademicSessionId,
            request.SessionDate,
            request.StartTime,
            request.EndTime);

        return await _repository.AddAsync(
            attendanceSession,
            cancellationToken);
    }
}