using AttendanceManagementSystem.Application.AttendanceSessions.DTOs;
using AttendanceManagementSystem.Application.Common.Interfaces;
using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Queries.GetAllAttendanceSessions;

public sealed class GetAllAttendanceSessionsQueryHandler
    : IRequestHandler<
        GetAllAttendanceSessionsQuery,
        IEnumerable<AttendanceSessionDto>>
{
    private readonly IAttendanceSessionRepository _repository;

    public GetAllAttendanceSessionsQueryHandler(
        IAttendanceSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AttendanceSessionDto>> Handle(
        GetAllAttendanceSessionsQuery request,
        CancellationToken cancellationToken)
    {
        var attendanceSessions = await _repository.GetAllAsync(
            cancellationToken);

        return attendanceSessions.Select(attendanceSession =>
            new AttendanceSessionDto
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
            });
    }
}