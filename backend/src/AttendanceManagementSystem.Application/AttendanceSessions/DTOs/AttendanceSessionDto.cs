namespace AttendanceManagementSystem.Application.AttendanceSessions.DTOs;

public sealed class AttendanceSessionDto
{
    public Guid Id { get; init; }

    public Guid CourseId { get; init; }

    public Guid LecturerId { get; init; }

    public Guid SemesterId { get; init; }

    public Guid AcademicSessionId { get; init; }

    public DateOnly SessionDate { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public bool IsOpen { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}