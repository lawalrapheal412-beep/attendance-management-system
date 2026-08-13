using MediatR;

namespace AttendanceManagementSystem.Application.AttendanceSessions.Commands.CreateAttendanceSession;

public sealed record CreateAttendanceSessionCommand(
    Guid CourseId,
    Guid LecturerId,
    Guid SemesterId,
    Guid AcademicSessionId,
    DateOnly SessionDate,
    TimeOnly StartTime,
    TimeOnly EndTime) : IRequest<Guid>;