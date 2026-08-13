using AttendanceManagementSystem.Application.AttendanceSessions.DTOs;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Queries.GetAttendanceSessionById;

public sealed class GetAttendanceSessionByIdQueryHandler
    : IRequestHandler<
        GetAttendanceSessionByIdQuery,
        AttendanceSessionDto?>
{
    private readonly IAttendanceSessionRepository _repository;

    public GetAttendanceSessionByIdQueryHandler(
        IAttendanceSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<AttendanceSessionDto?> Handle(
        GetAttendanceSessionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var attendanceSession = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (attendanceSession is null)
        {
            return null;
        }

        return new AttendanceSessionDto
        {
            Id = attendanceSession.Id,
            CourseId = attendanceSession.CourseId,
            LecturerId = attendanceSession.LecturerId,
            SemesterId = attendanceSession.SemesterId,
            AcademicSessionId = attendanceSession.AcademicSessionId,
            SessionDate = attendanceSession.SessionDate,
            StartTime = attendanceSession.StartTime,
            EndTime = attendanceSession.EndTime,
            IsOpen = attendanceSession.IsOpen,
            CreatedAt = attendanceSession.CreatedAt,
            UpdatedAt = attendanceSession.UpdatedAt
        };
    }
}